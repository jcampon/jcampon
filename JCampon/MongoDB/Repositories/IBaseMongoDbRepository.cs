using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq.Expressions;
using JCampon.MongoDB.Entities;

namespace JCampon.MongoDB.Repositories
{
	public interface IBaseMongoDbRepository<T, TId> where T : MongoDbAggregateRoot<TId>, IMongoDbAggregateRoot
    {
        /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		Task<TId> AddOneAsync(T entity);

        /// <summary>
		/// get all records
		/// </summary>
		/// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
		Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		Task<T> GetByIdAsync(TId id);

        /*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<ReplaceOneResult> UpdateOneAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		Task<DeleteResult> DeleteOneAsync(T entity);
        */
    }
}
