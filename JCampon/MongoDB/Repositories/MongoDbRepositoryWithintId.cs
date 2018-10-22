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
	public abstract class MongoDbRepositoryWithIntId<T> : BaseMongoDbRepository<T, int>, 
                                                          IBaseMongoDbRepository<T, int> where T : MongoDbEntityWithIntId, IMongoDbAggregateRoot
    {
	    protected MongoDbRepositoryWithIntId(IMongoDbDatabaseContext dbContext, string collectionName) : base(dbContext, collectionName)
        {
        }
    }
}
