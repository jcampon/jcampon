﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JCampon.MongoDB.Repositories;

namespace JCampon.MongoDB.Tests
{	
	public class BaseTestsForMongoDbRepositories
	{
		protected MongoDbDatabaseContext DbContext;

		public BaseTestsForMongoDbRepositories()
		{
			DbContext = new MongoDbDatabaseContext("mongodb://localhost:27017", "JCamponTestDatabase");
		}
	}
}