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

	public abstract class MongoRepositoryWithIntId<TMongoDbEntityWithIntId> : MongoRepository<TMongoDbEntityWithIntId>, IMongoRepositoryWithIntId<TMongoDbEntityWithIntId> where TMongoDbEntityWithIntId : MongoDbEntityWithIntId
    {
		/// <summary>
		/// Adds a new record
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public new async Task<int> Add(TMongoDbEntityWithIntId entity)
		{
			await Collection.InsertOneAsync(entity);

			return entity.Id;
		}

		public Task<int> GetNextSequenceValue()
		{
			throw new NotImplementedException();
		}
    }
}
