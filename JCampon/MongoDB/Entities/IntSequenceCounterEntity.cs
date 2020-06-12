using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JCampon.MongoDB.Entities
{
	public class IntSequenceCounterEntity : MongoDbEntityWithStringId
    {
        public IntSequenceCounterEntity(string databaseName, string collectionName)
        {
            if(String.IsNullOrWhiteSpace(databaseName))
                   throw new ArgumentNullException("Error! The value of 'databaseName' cannot be null or empty.");

            if (String.IsNullOrWhiteSpace(collectionName))
                throw new ArgumentNullException("Error! The value of 'collectionName' cannot be null or empty.");

            DatabaseName = databaseName;
            CollectionName = collectionName;

            Id = DatabaseName + "_" + CollectionName;
        }

        private IntSequenceCounterEntity()
        {
            /* Made private default constructor to enforce usage of public overloaded version */
        }

        public string DatabaseName { get; }
		public string CollectionName { get; }
		public int CurrentValueOnIntSequenceCounter { get; set; }
    }
}
