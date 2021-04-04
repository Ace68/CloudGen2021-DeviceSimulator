using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CloudGenDeviceSimulator.ReadModel.Abstracts
{
    public interface IPersister
    {
        Task<T> GetByIdAsync<T>(string id) where T : DtoBase;
        Task InsertAsync<T>(T dtoToInsert) where T : DtoBase;
        Task UpdateAsync<T>(T dtoToUpdate) where T : DtoBase;
        Task DeleteAsync<T>(string id) where T : DtoBase;
        Task<IEnumerable<T>> FindAsync<T>(Expression<Func<T, bool>> filter = null) where T : DtoBase;
        Task<IEnumerable<T>> FindLastDocumentsAsync<T>(Expression<Func<T, bool>> filter = null, string orderBy = null,
            bool @ascending = true, int limit = 0) where T : DtoBase;
    }
}