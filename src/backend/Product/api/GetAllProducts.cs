using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using eShop.Products.Api.Infrastructure;
using eShop.Products.Api.Model;
using eShop.Products.Api.Repository;
using eShop.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace eShop.Products.Api
{
    public class GetAllProducts
    {
        
        [Function("GetAllProducts")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("GetAllProducts");
            logger.LogInformation("C# HTTP trigger function processed a request.");

            SearchProductModel pageModel = new SearchProductModel
            {
                PageNo = 1,
                PageSize = 10,
                SearchTerm = string.Empty,
                CategoryTerm = string.Empty
            };

            string pageData = req.ReadAsString();

            //pageData = new RequestUtility().GetJsonFromRequest(req);

            if (!string.IsNullOrWhiteSpace(pageData))
			{
                var model = JsonSerializerExtensions.CaseInsensitiveDeseialize<SearchProductModel>(pageData);

                if (model != null)
                {
                    pageModel.PageNo = model.PageNo > 0 ? model.PageNo: pageModel.PageNo;
                    pageModel.PageSize = model.PageSize > 0 ? model.PageSize :  pageModel.PageSize;
                    pageModel.SearchTerm = string.IsNullOrWhiteSpace(model.SearchTerm) ? "": model.SearchTerm;
                    pageModel.CategoryTerm = string.IsNullOrWhiteSpace(model.CategoryTerm) ? "": model.CategoryTerm;
                }
			}

            var repository = executionContext.InstanceServices.GetService(typeof(IProductRepository)) as IProductRepository;

            var data = repository.GetAllProducts(pageModel).Select(s => new { 
                ProductId = s.ProductId,
                ProductName = s.ProductName,
                Category = s.Category.CategoryName,
                AvailableQuantity = s.AvailableQuantity,
                Description = s.Description,
                Image = s.Image,
                Price = s.Price
            }).ToList();

			var response = req.CreateResponse(HttpStatusCode.OK);

			response.WriteString(JsonSerializer.Serialize(data));

            return response;

        }
    }

    
}
