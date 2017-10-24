using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JCampon.MongoDB.Entities
{
	public class IntSequenceCounter
    {
        [BsonId]
        public ObjectId Id { get; set; }
		public string DatabaseName { get; set; }
		public string CollectionName { get; set; }
		public int CurrentValueOnIntSequenceCounter { get; set; }
    }
}
