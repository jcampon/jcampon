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
	public abstract class MongoDbRepositoryWithObjectId<T> : BaseMongoDbRepository<T, ObjectId>, 
                                                             IBaseMongoDbRepository<T, ObjectId> where T : MongoDbEntity, IMongoDbAggregateRoot
    {
	    protected MongoDbRepositoryWithObjectId(IMongoDbDatabaseContext dbContext, string collectionName) : base(dbContext, collectionName)
        {
        }

	    /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		public new virtual async Task<ObjectId> AddOneAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);

            return entity.Id;
        }

		/// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		public new virtual async Task<T> GetByIdAsync(ObjectId id)
        {
            var result = await Collection.FindAsync(doc => doc.Id.Equals(id));

            return result.FirstOrDefault();
        }
		
		/// <summary>
        /// get all records
        /// </summary>
        /// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
		public new virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            // return await Collection.Find(_ => true).ToListAsync(); 
            return await Collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        /*
		/// <summary>
		/// 
		/// </summary>
		/// <param name="updatedEntity"></param>
		/// <returns></returns>
		public virtual async Task<ReplaceOneResult> Update(TMongoDbEntity updatedEntity)
		{
			var filter = Builders<TMongoDbEntity>.Filter.Eq("Id", updatedEntity.Id);
			//var update = Builders<TMongoDbEntity>.Update.Set("Name", windFarm.Name);
			//update.AddToSet("UpdatedOn", DateTime.Now);
			
			var result = await Collection.ReplaceOneAsync(filter, updatedEntity);

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityToDelete"></param>
		/// <returns></returns>
		public virtual async Task<DeleteResult> Delete(TMongoDbEntity entityToDelete)
		{
			var filter = Builders<TMongoDbEntity>.Filter.Eq("Id", entityToDelete.Id);

			var result = await Collection.DeleteOneAsync(filter);

			return result;			
		}
        */
    }
}
