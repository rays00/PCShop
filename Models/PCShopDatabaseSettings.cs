using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCShop.Models
{
    public class PCShopDatabaseSettings : IPCShopDatabaseSettings
    {
        public string UserCollectionName { get; set; }
        public string ProductCollectionName { get; set; }
        public string OrderCollectionName { get; set; }
        public string EstimatedShippingCollectionName { get; set; }
        public string VendorCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }


    public interface IPCShopDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string ProductCollectionName { get; set; }
        string OrderCollectionName { get; set; }
        string EstimatedShippingCollectionName { get; set; }
        string VendorCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
