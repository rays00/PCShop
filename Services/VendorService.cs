using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using PCShop.Models;

namespace PCShop.Services
{
    public class VendorService
    {
        private readonly IMongoCollection<Vendor> _vendor;

        public VendorService(IPCShopDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _vendor = database.GetCollection<Vendor>(settings.VendorCollectionName);
        }

        public List<Vendor> Get() =>
            _vendor.Find(user => true).ToList();

        public Vendor Get(string id) =>
            _vendor.Find<Vendor>(vendor => vendor.Id == id).FirstOrDefault();

        public Vendor Create(Vendor vendor)
        {
            _vendor.InsertOne(vendor);
            return vendor;
        }

        public void Update(string id, Vendor vendorIn) =>
           _vendor.ReplaceOne(vendor => vendor.Id == id, vendorIn);

        public void Remove(Vendor vendorIn) =>
            _vendor.DeleteOne(vendor => vendor.Id == vendorIn.Id);

        public void Remove(string id) =>
            _vendor.DeleteOne(vendor => vendor.Id == id);
    }
}
