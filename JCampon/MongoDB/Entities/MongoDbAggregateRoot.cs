using MongoDB.Bson.Serialization.Attributes;

namespace JCampon.MongoDB.Entities
{
    public abstract class MongoDbAggregateRoot<TId> : IMongoDbAggregateRoot
    {
        private TId _id;

        [BsonId]
        public virtual TId Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
