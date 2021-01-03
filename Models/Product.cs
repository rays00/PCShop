using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PCShop.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Price")]
        public decimal Price { get; set; }

        [BsonElement("VendorIds")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> VendorIds { get; set; }

        [BsonElement("Vendors")]
        public List<Vendor> Vendors { get; set; }
    }
}
