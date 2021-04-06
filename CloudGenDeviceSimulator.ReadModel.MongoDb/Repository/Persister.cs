using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CloudGenDeviceSimulator.ReadModel.Abstracts;
using CloudGenDeviceSimulator.Shared.Services;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace CloudGenDeviceSimulator.ReadModel.MongoDb.Repository
{
    public class Persister : IPersister
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly ILogger _logger;

        public Persister(IMongoDatabase mongoDatabase, ILoggerFactory loggerFactory)
        {
            this._mongoDatabase = mongoDatabase;
            this._logger = loggerFactory.CreateLogger(this.GetType());
        }

        public async Task<T> GetByIdAsync<T>(string id) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

                var results = collection.Where(t => t.Id.Equals(id));
                return await results.AnyAsync()
                    ? results.First()
                    : null;
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task InsertAsync<T>(T dtoToInsert) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                await collection.InsertOneAsync(dtoToInsert);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task UpdateAsync<T>(T dtoToUpdate) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                await collection.ReplaceOneAsync(x => x.Id == dtoToUpdate.Id, dtoToUpdate);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task DeleteAsync<T>(string id) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>());
                var filter = Builders<T>.Filter.Eq("_id", id);
                await collection.FindOneAndDeleteAsync(filter);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filter = null) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

                return await Task.Run(() => filter != null
                    ? collection.Where(filter)
                    : collection);
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        public async Task<IEnumerable<T>> FindLastDocumentsAsync<T>(Expression<Func<T, bool>> filter = null,
            string orderBy = null,
            bool @ascending = true, int limit = 0) where T : DtoBase
        {
            try
            {
                var collection = this._mongoDatabase.GetCollection<T>(GetCollectionName<T>()).AsQueryable();

                collection = await Task.Run(() => filter != null
                    ? collection.Where(filter)
                    : collection);

                collection = await Task.Run(() => !string.IsNullOrWhiteSpace(orderBy)
                    ? collection.OrderByField(orderBy, @ascending)
                    : collection);

                collection = limit > 0
                    ? collection.Take(limit)
                    : collection;

                return collection;
            }
            catch (Exception ex)
            {
                this._logger.LogError(CommonServices.GetDefaultErrorTrace(ex));
                throw;
            }
        }

        private static string GetCollectionName<T>() where T : DtoBase => typeof(T).Name;
    }
}