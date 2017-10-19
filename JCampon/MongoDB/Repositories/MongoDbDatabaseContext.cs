using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JCampon.MongoDB.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace JCampon.MongoDB.Repositories
{
    public abstract class MongoDbDatabaseContext: IMongoDbDatabaseContext
    {
        private readonly IMongoDatabase _database = null;
        private const string databaseName = "test";

        public MongoDbDatabaseContext(string connectionString)
        {
			var client = new MongoClient(connectionString);
            if (client == null)
                throw new MongoClientException("A new MongoDB client instance could not be created from the connection string settings provided");

            _database = client.GetDatabase(databaseName);
        }

        public IMongoDatabase Database
        {
            get
            {
                return _database;
            }
        }
    }
}
