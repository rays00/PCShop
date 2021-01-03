using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PCShop.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        [BsonElement("ProductIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> ProductIds { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("Total")]
        public int total { get; set; }

        [BsonElement("ProductNames")]
        public List<string> ProductNames { get; set; }
    }
}
