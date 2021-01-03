using PCShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PCShop.Services
{
    public class EstimatedShippingService
    {
        private readonly IMongoCollection<EstimatedShipping> _estimatedShipping;

        public EstimatedShippingService(IPCShopDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _estimatedShipping = database.GetCollection<EstimatedShipping>(settings.EstimatedShippingCollectionName);
        }

        public List<EstimatedShipping> Get() =>
            _estimatedShipping.Find(estimatedShipping => true).ToList();

        public EstimatedShipping Get(string id) =>
            _estimatedShipping.Find<EstimatedShipping>(estimatedShipping => estimatedShipping.Id == id).FirstOrDefault();

        public EstimatedShipping Create(EstimatedShipping estimatedShipping)
        {
            _estimatedShipping.InsertOne(estimatedShipping);
            return estimatedShipping;
        }

        public void Update(string id, EstimatedShipping estimatedShippingIn) =>
            _estimatedShipping.ReplaceOne(estimatedShipping => estimatedShipping.Id == id, estimatedShippingIn);

        public void Remove(EstimatedShipping estimatedShippingIn) =>
            _estimatedShipping.DeleteOne(estimatedShipping => estimatedShipping.Id == estimatedShippingIn.Id);

        public void Remove(string id) =>
            _estimatedShipping.DeleteOne(estimatedShipping => estimatedShipping.Id == id);
    }
}