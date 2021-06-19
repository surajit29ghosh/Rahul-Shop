using System;
using System.Collections.Generic;
using System.Linq;
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
    public static class GetCategories
    {
        [Function("GetCategories")]
        public static HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
            FunctionContext executionContext)
        {
            try
            {


                var logger = executionContext.GetLogger("GetCategories");
                logger.LogInformation("C# HTTP trigger function processed a request.");

                PageModel pageModel = new PageModel
                {
                    PageNo = 0,
                    PageSize = 0
                };

                string pageData = req.ReadAsString();

                //pageData = new RequestUtility().GetJsonFromRequest(req);

                if (!string.IsNullOrWhiteSpace(pageData))
                {
                    var model = JsonSerializerExtensions.CaseInsensitiveDeseialize<PageModel>(pageData);

                    if (model != null)
                    {
                        pageModel.PageNo = model.PageNo > 0 ? model.PageNo : pageModel.PageNo;
                        pageModel.PageSize = model.PageSize > 0 ? model.PageSize : pageModel.PageSize;
                    }
                }

                var repository = executionContext.InstanceServices.GetService(typeof(ICategoryRepository)) as ICategoryRepository;

                var data = repository.GetCategoriesWithProducts(pageModel).Select(s => new {
                    CategoryId = s.CategoryId,
                    CategoryName = s.CategoryName,
                    TotalProducts = s.Products.Count()
                }).ToList();

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
