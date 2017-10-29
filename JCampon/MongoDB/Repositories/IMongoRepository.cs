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
	public interface IMongoRepository<TMongoDbEntity> where TMongoDbEntity : IMongoDbEntity
    {
        /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		Task<ObjectId> Add(TMongoDbEntity entity);

        /// <summary>
		/// get all records
		/// </summary>
		/// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
		Task<IEnumerable<TMongoDbEntity>> GetAll();

        /// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		Task<TMongoDbEntity> GetById(ObjectId id);

    }
}
