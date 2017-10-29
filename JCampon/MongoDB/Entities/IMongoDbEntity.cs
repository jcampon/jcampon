using MongoDB.Bson;

namespace JCampon.MongoDB.Entities
{
	public interface IMongoDbEntity
    {
		ObjectId Id { get; set; }
    }
}
