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
        private const string CollectionName = "test";
        public readonly IMongoCollection<TEntity> Collection;

        public MongoRepository(IMongoDbDatabaseContext dbContext)
        {
            _dbContext = dbContext;
            Collection = _dbContext.Database.GetCollection<TEntity>(CollectionName);
        }

        /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ObjectId> Add(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);

            return entity._id;
        }

        /// <summary>
		/// Find an entity by its Id
		/// </summary>
		/// <param name="id">Id of entity</param>
		/// <returns>If Id matches an entity in the database, returns entity. If no matches are found, returns null</returns>
		public async Task<TEntity> GetById(ObjectId id)
        {
            var filter = Builders<TEntity>.Filter.Eq("Id", id);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        /// <summary>
        /// get all records
        /// </summary>
        /// <returns>return all entities in the database, returns entity. If no matches are found, returns null</returns>
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Collection.Find(_ => true).ToListAsync();
        }

    }
}
