using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace JCampon.MongoDB.Entities
{
    public class CounterSequence
    {
        [BsonId]
        public string Id { get; set; }
        public int Sequence { get; set; }
    }
}
