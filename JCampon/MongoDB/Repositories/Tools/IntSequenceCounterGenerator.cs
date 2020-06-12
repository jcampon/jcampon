using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace JCampon.MongoDB.Repositories.Tools
{
    public class IntSequenceCounterGenerator : IIdGenerator
    {
        private readonly IntSequenceCounterRepository _intSequenceCounterRepository;

        public IntSequenceCounterGenerator(IntSequenceCounterRepository intSequenceCounterRepository)
        {
            _intSequenceCounterRepository  = intSequenceCounterRepository;           
        }

        public object GenerateId(object container, object document)
        {
            var collection = container as IMongoCollection<BsonDocument>;

            if (collection == null)
                return 0;

            var nextNumber = _intSequenceCounterRepository.GetNextNumber();

            return nextNumber;
        }

        /// <summary>
        /// Check if the Id has been set to an integer or not
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsEmpty(object id)
        {
            // if the id is null or not an integer - say its empty
            if (id == null || !(id is int)) return true;

            // if the id is 0 or less - say its empty
            return ((int)id) <= 0;
        }
    }
}
