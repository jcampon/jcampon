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

	public abstract class MongoRepositoryWithIntId<TMongoDbEntityWithIntId> : MongoRepository where TMongoDbEntityWithIntId : MongoDbEntityWithIntId
    {

		/// <summary>
		/// Adds a new record
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public async Task<int> Add(TMongoDbEntityWithIntId entity)
		{
			await Collection.InsertOneAsync(entity);

			return entity._id;
		}    
    }
}
