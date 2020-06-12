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
	public abstract class BaseMongoDbRepository<T, TId> : IBaseMongoDbRepository<T, TId> where T : MongoDbAggregateRoot<TId>, IMongoDbAggregateRoot
    {
		public readonly string CollectionName;
        public readonly string DatabaseName;

        protected readonly IMongoCollection<T> Collection;

	    protected BaseMongoDbRepository(IMongoCollection<T> collection)
        {
			if (collection == null)
				throw new ArgumentNullException("collection", "ERROR! the parameter collection cannot be NULL");

			Collection = collection;
            CollectionName = collection.CollectionNamespace.CollectionName;
            DatabaseName = collection.Database.DatabaseNamespace.DatabaseName;
        }

	    /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
		public virtual async Task<TId> AddOneAsync(T entity)
        {
            try
            {
                await Collection.InsertOneAsync(entity);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return entity.Id;
        }

		/// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		public virtual async Task<T> GetOneByIdAsync(TId id)
        {
            var filter = Builders<T>.Filter.Eq(entity => entity.Id, id);
            var result = await Collection.FindAsync(filter).Result.FirstOrDefaultAsync();

            return result;            
        }
		
		/// <summary>
        /// get all records
        /// </summary>
        /// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
		public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            // return await Collection.Find(_ => true).ToListAsync(); 
            return await Collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }
        
		/// <summary>
		/// 
		/// </summary>
		/// <param name="updatedEntity"></param>
		/// <returns></returns>
		public virtual async Task<T> FindOneAndReplaceAsync(T updatedEntity)
		{
			var filter = Builders<T>.Filter.Eq(t => t.Id, updatedEntity.Id);
		    var options = new FindOneAndReplaceOptions<T>() { ReturnDocument = ReturnDocument.After };    

		    var result = await Collection.FindOneAndReplaceAsync(filter, updatedEntity, options);

			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="entityToDelete"></param>
		/// <returns></returns>
		public virtual async Task<DeleteResult> DeleteOneAsync(T entityToDelete)
		{
			var filter = Builders<T>.Filter.Eq(t => t.Id, entityToDelete.Id);

			var result = await Collection.DeleteOneAsync(filter);

			return result;			
		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entitiesToDelete"></param>
        /// <returns></returns>
        public virtual async Task<DeleteResult> DeleteManyAsync(IEnumerable<TId> listOfIdsToDelete)
        {
            var filter = Builders<T>.Filter.In(entity => entity.Id, listOfIdsToDelete);

            var result = await Collection.DeleteManyAsync(filter);

            return result;
        }

    }
}
