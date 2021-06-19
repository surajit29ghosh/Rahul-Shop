using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text.Json;
using eShop.Cart.API.Models;
using eShop.Cart.API.Repositories;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using eShop.Utilities;
using System.Linq;

namespace eShop.Cart.API
{
    public static class GetCart
    {
        [Function("GetCart")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            try
            {
                var logger = executionContext.GetLogger("GetCart");
                logger.LogInformation("C# HTTP trigger function processed a request.");

                string input = req.ReadAsString();

                //input = new RequestUtility().GetJsonFromRequest(req);

                if (string.IsNullOrWhiteSpace(input))
                {
                    var error = req.CreateResponse(HttpStatusCode.BadRequest);
                    error.WriteString("Required parameters missing");
                    return error;
                }

                GetCartModel model = JsonSerializerExtensions.CaseInsensitiveDeseialize<GetCartModel>(input);

                var repository = executionContext.InstanceServices.GetService(typeof(ICacheRepository)) as ICacheRepository;

                IEnumerable<CacheItemEntity> cart = repository.GetCart(model.CartId);

                var data = cart.Select(c => new
                {
                    ProductId = c.RowKey,
                    ProductName = c.ProductName,
                    Units = c.Cartitem
                });

                var response = req.CreateResponse(HttpStatusCode.OK);

                response.WriteString(JsonSerializer.Serialize(data));

                return response;
            }
            catch (Exception e)
            {
                var response = req.CreateResponse(HttpStatusCode.InternalServerError);
                response.WriteString(e.StackTrace);
                return response;

            }
        }
    }
}
