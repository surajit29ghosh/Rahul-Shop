using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using eShop.Products.Api.Model;
using eShop.Products.Api.Repository;
using eShop.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace eShop.Products.Api
{
    public static class ProductDetails
    {
        [Function("ProductDetails")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("ProductDetails");
            logger.LogInformation("C# HTTP trigger function processed a request.");

          
            string productId = string.Empty;
            string pageData = req.ReadAsString();

            //pageData = new RequestUtility().GetJsonFromRequest(req);

            if (!string.IsNullOrWhiteSpace(pageData))
            {
                productId = JsonSerializer.Deserialize<ProductRequest>(pageData).ProductId;
            }

            var repository = executionContext.InstanceServices.GetService(typeof(IProductRepository)) as IProductRepository;

            var data = repository.GetProduct(productId);

            var filter = new {
                     ProductId = data.ProductId,
                     ProductName = data.ProductName,
                     Category = data.Category.CategoryName,
                     AvailableQuantity = data.AvailableQuantity,
                     Description = data.Description,
                     Image = data.Image,
                     Price = data.Price
                };

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.WriteString(JsonSerializer.Serialize(filter));

            return response;
        }

        
    }
}
