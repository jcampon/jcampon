using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JCampon.MongoDB.Entities;
using MongoDB.Driver;
using MongoDB.Bson;

namespace JCampon.MongoDB.Repositories
{
    public class MongoDbDatabaseContext: IMongoDbDatabaseContext
    {
        private readonly IMongoDatabase _database = null;
        private readonly string _databaseName = "test";

		public MongoDbDatabaseContext(string connectionString, string databaseName)
        {
			var client = new MongoClient(connectionString);
            if (client == null)
                throw new MongoClientException("A new MongoDB client instance could not be created from the connection string settings provided");

			if (String.IsNullOrWhiteSpace(databaseName))
				throw new MongoClientException("A new MongoDB client instance could not be created from the database name provided");

			_databaseName = databaseName;
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
