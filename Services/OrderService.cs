using PCShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PCShop.Services
{
    public class OrderService
    {
        private readonly IMongoCollection<Order> _order;

        public OrderService(IPCShopDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _order = database.GetCollection<Order>(settings.OrderCollectionName);
        }

        public List<Order> Get() =>
            _order.Find(product => true).ToList();

        public Order Get(string id) =>
            _order.Find<Order>(order => order.Id == id).FirstOrDefault();

        public List<Order> GetByUserId(string userId) =>
            _order.Find(order => order.UserId == userId).ToList();

        public Order Create(Order order)
        {
            _order.InsertOne(order);
            return order;
        }

        public void Update(string id, Order orderIn) =>
            _order.ReplaceOne(order => order.Id == id, orderIn);

        public void Remove(Order orderIn) =>
            _order.DeleteOne(order => order.Id == orderIn.Id);

        public void Remove(string id) =>
            _order.DeleteOne(order => order.Id == id);
    }
}
