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
    public abstract class MongoRepository : IMongoRepository<MongoDbEntity>
    {
        private readonly IMongoDbDatabaseContext _dbContext;
        public readonly string CollectionName = "test";
		public readonly IMongoCollection<MongoDbEntity> Collection;

	    protected MongoRepository(IMongoDbDatabaseContext dbContext, string collectionName)
        {
            _dbContext = dbContext;
			CollectionName = collectionName;
			Collection = _dbContext.Database.GetCollection<MongoDbEntity>(CollectionName);
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
		public virtual async Task<ObjectId> Add(MongoDbEntity entity)
        {
            await Collection.InsertOneAsync(entity);

            return entity._id;
        }

        /// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		public virtual async Task<MongoDbEntity> GetById(ObjectId id)
        {
			var filter = Builders<MongoDbEntity>.Filter.Eq("Id", id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get all records
        /// </summary>
        /// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
		public virtual async Task<IEnumerable<MongoDbEntity>> GetAll()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

    }
}
