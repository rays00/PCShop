using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PCShop.Models
{
    public class EstimatedShipping
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("OrderId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }

        [BsonElement("EstimatedDate")]
        [BsonDateTimeOptions(DateOnly = true)]
        public DateTime EstimatedDate { get; set; }
    }
}
