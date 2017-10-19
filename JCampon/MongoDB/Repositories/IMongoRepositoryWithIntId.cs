using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace JCampon.MongoDB.Repositories
{
    public interface IMongoRepositoryWithIntId<Int32>
    {
        /// <summary>
        /// Adds a new record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        new Task<int> Add(int entity);

        /// <summary>
        /// Generates the next INT ID value on the related sequence for a new record
        /// </summary>
        /// <returns></returns>
        Task<int> GetNextSequenceValue();
    }

}
