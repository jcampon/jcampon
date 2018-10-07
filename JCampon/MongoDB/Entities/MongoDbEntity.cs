using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace JCampon.MongoDB.Entities
{
	public abstract class MongoDbEntity : MongoDbAggregateRoot<ObjectId>, IMongoDbAggregateRoot
    {

    }
}
