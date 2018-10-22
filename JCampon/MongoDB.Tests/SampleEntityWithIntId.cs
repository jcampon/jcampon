using JCampon.MongoDB.Entities;

namespace JCampon.MongoDB.Tests
{
    public class SampleEntityWithIntId : MongoDbEntityWithIntId
    {
        public string EntityName { get; set; }
    }
}