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
	public abstract class MongoDbRepositoryWithStringId<T> : BaseMongoDbRepository<T, string> where T : MongoDbEntityWithStringId
    {
	    protected MongoDbRepositoryWithStringId(IMongoCollection<T> collection) : base(collection)
        {
            /*             
             */
        }
    }
}
