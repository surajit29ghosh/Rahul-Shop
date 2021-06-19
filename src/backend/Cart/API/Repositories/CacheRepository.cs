using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Azure.Data.Tables;
using eShop.Cart.API.Models;

namespace eShop.Cart.API.Repositories
{
    public interface ICacheRepository
    {
         IEnumerable<CacheItemEntity> GetCart(string customerId);
         string AddToCart(AddCartModel model);
         string RemoveFromCart(AddCartModel model);

    }
    public class TableCacheRepository: ICacheRepository
    {
        private readonly IConfiguration configuration;

        public TableCacheRepository(IConfiguration config)
        {
            this.configuration = config;
        }

        public IEnumerable<CacheItemEntity> GetCart(string customerId)
        {
            var table = this.GetCacheTable().Result;

            //Azure.Pageable<CacheItemEntity> items = table.Query<CacheItemEntity>(cache => cache.PartitionKey.Equals(customerId));
            Azure.Pageable<CacheItemEntity> items = table.Query<CacheItemEntity>(filter: $"PartitionKey eq '{customerId}'");
        
            return items;
        }

        public string AddToCart(AddCartModel model)
        {
            var table = this.GetCacheTable().Result;
            CacheItemEntity item;

            if(!string.IsNullOrWhiteSpace(model.CartId))
                item = this.GetCacheItem(model.CartId, model.ProductId).Result;
            else
            {
                item = null;
                model.CartId = Guid.NewGuid().ToString();
            }

            if(item != null)
            {
                item.Cartitem += model.Units;

            }
            else
            {
                item = new CacheItemEntity(model.CartId, model.ProductId)
                {
                    ProductName = model.ProductName,
                    Cartitem = model.Units,
                    ETag = new Azure.ETag(model.CartId),
                    Timestamp = System.DateTimeOffset.Now,
                };
            }

            table.UpsertEntity(item, TableUpdateMode.Replace);

            return model.CartId;
        }

        public string RemoveFromCart(AddCartModel model)
        {
            var table = this.GetCacheTable().Result;
            CacheItemEntity item = null;

            if (!string.IsNullOrWhiteSpace(model.CartId))
                item = this.GetCacheItem(model.CartId, model.ProductId).Result;

            if (item != null)
                item.Cartitem -= model.Units;

            table.UpsertEntity(item, TableUpdateMode.Replace);

            return model.CartId;
        }

        private async Task<TableClient> GetCacheTable()
        {
            var client = new TableClient(
                                configuration["CacheConnectionString"],
                                configuration["CacheTable"]);
            
            await client.CreateIfNotExistsAsync();

            return client;
        }

        private async Task<CacheItemEntity> GetCacheItem(string customerId, string productId)
        {
            var client = await GetCacheTable();

            Azure.Pageable<CacheItemEntity> items = client.Query<CacheItemEntity>(c => c.PartitionKey.Equals(customerId) && c.RowKey.Equals(productId));

            if (items.GetEnumerator().Current != null)
                return items.GetEnumerator().Current;
            else
                return null;
            //var item = await client.GetEntityAsync<CacheItemEntity>(customerId, productId);

            //return item;

        }
    }
}