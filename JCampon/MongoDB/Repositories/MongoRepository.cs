using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq.Expressions;
using JCampon.MongoDB.Entities;

namespace JCampon.MongoDB.Repositories
{
	public abstract class MongoRepository<TMongoDbEntity> : IMongoRepository<TMongoDbEntity> where TMongoDbEntity : IMongoDbEntity
	{
		public readonly string CollectionName;
		protected readonly IMongoCollection<TMongoDbEntity> Collection;

	    protected MongoRepository(IMongoDbDatabaseContext dbContext, string collectionName)
        {
			if (dbContext == null)
				throw new ArgumentNullException("dbContext", "ERROR! the parameter dbContext cannot be NULL");

			if(string.IsNullOrWhiteSpace(collectionName))
				throw new ArgumentNullException("collectionName", "ERROR! the parameter collectionName cannot be NULL");

		    CollectionName = collectionName;
			Collection = dbContext.Database.GetCollection<TMongoDbEntity>(collectionName);
        }

	    protected MongoRepository()
	    {
		    throw new NotImplementedException();
	    }

	    /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		public virtual async Task<ObjectId> Add(TMongoDbEntity entity)
        {
            await Collection.InsertOneAsync(entity);

            return entity.Id;
        }

		/// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		public virtual async Task<TMongoDbEntity> GetById(ObjectId id)
        {
			var filter = Builders<TMongoDbEntity>.Filter.Eq("Id", id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
		
		/// <summary>
        /// get all records
        /// </summary>
        /// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
		public virtual async Task<IEnumerable<TMongoDbEntity>> GetAll()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

		public virtual async Task<ReplaceOneResult> Update(TMongoDbEntity updatedEntity)
		{
			var filter = Builders<TMongoDbEntity>.Filter.Eq("Id", updatedEntity.Id);
			//var update = Builders<TMongoDbEntity>.Update.Set("Name", windFarm.Name);
			//update.AddToSet("UpdatedOn", DateTime.Now);
			
			var result = await Collection.ReplaceOneAsync(filter, updatedEntity);

			return result;
		}

    }
}
