﻿using PCShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace PCShop.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _product;

        public ProductService(IPCShopDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _product = database.GetCollection<Product>(settings.ProductCollectionName);
        }

        public List<Product> Get() =>
            _product.Find(product => true).ToList();

        public Product Get(string id) =>
            _product.Find<Product>(product => product.Id == id).FirstOrDefault();

        public Product Create(Product product)
        {
            _product.InsertOne(product);
            return product;
        }

        public void Update(string id, Product productIn) =>
            _product.ReplaceOne(product => product.Id == id, productIn);

        public void Remove(Product productIn) =>
            _product.DeleteOne(product => product.Id == productIn.Id);

        public void Remove(string id) =>
            _product.DeleteOne(product => product.Id == id);
    }
}