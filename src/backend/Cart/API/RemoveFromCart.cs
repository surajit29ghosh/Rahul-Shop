using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using eShop.Cart.API.Models;
using eShop.Cart.API.Repositories;
using eShop.Utilities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace eShop.Cart.API
{
    public static class RemoveFromCart
    {
        [Function("RemoveFromCart")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            try
            {
                var logger = executionContext.GetLogger("RemoveFromCart");
                logger.LogInformation("C# HTTP trigger function processed a request.");

                string input = req.ReadAsString();

                //input = new RequestUtility().GetJsonFromRequest(req);

                if (string.IsNullOrWhiteSpace(input))
                {
                    var error = req.CreateResponse(HttpStatusCode.BadRequest);
                    error.WriteString("Required parameters missing");
                    return error;
                }

                AddCartModel model = JsonSerializerExtensions.CaseInsensitiveDeseialize<AddCartModel>(input);

                var repository = executionContext.InstanceServices.GetService(typeof(ICacheRepository)) as ICacheRepository;

                string cartId = repository.RemoveFromCart(model);

                var data = new
                {
                    CartId = cartId,
                };

                var response = req.CreateResponse(HttpStatusCode.OK);

                response.WriteString(JsonSerializer.Serialize(data));

                return response;
            }
            catch(Exception ex)
			{
                var error = req.CreateResponse(HttpStatusCode.InternalServerError);
                error.WriteString(ex.StackTrace);
                return error;
            }
        }
    }
}
