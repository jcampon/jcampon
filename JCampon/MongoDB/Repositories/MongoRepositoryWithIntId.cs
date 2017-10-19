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
    //public abstract class MongoRepositoryWithIntId<TEntity> : MongoRepository<TEntity>, IMongoRepository<TEntity> where TEntity : IMongoDbEntity<ObjectId>
    public abstract class MongoRepositoryWithIntId<TEntity, TId> : MongoRepository<TEntity, TId>, IMongoRepository<TEntity, TId> where TEntity : IMongoDbEntity<int>
    {
        public MongoRepositoryWithIntId(IMongoDbDatabaseContext dbContext) : base(dbContext)
        {
        }
    }
}
