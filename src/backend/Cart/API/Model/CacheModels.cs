using System;
using Azure.Data.Tables;

namespace eShop.Cart.API.Models
{
    public class CacheItemEntity : ITableEntity
    {
        public CacheItemEntity()
        { }
        public CacheItemEntity(string customerId, string productId)
        {
            PartitionKey = customerId;
            RowKey = productId;
        }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
		public string ProductName { get; set; }
		public int Cartitem { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public Azure.ETag ETag { get; set; }

    }
}