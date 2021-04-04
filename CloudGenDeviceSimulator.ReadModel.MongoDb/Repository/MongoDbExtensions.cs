using System;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Driver.Linq;

namespace CloudGenDeviceSimulator.ReadModel.MongoDb.Repository
{
    public static class MongoDbExtensions
    {
        public static IMongoQueryable<T> OrderByField<T>(this IMongoQueryable<T> q, string sortField, bool @ascending)
        {
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, sortField);
            var expression = Expression.Lambda(property, parameter);
            var method = @ascending ? "OrderBy" : "OrderByDescending";
            var types = new Type[] { q.ElementType, expression.Body.Type };
            var methodCallExpression = Expression.Call(typeof(Queryable), method, types, q.Expression, expression);

            return (IMongoQueryable<T>) q.Provider.CreateQuery<T>(methodCallExpression);
        }
    }
}