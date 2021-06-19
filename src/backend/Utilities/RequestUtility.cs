using Microsoft.Azure.Functions.Worker.Http;
using System;
using System.Collections.Specialized;
using System.Text;
using System.Text.Json;

namespace eShop.Utilities
{
	public class RequestUtility
	{
		public string GetJsonFromRequest(HttpRequestData req)
		{
            string input = string.Empty;
            

            if (req.Method == "GET")
            {
                string query = req.Url.Query;
                NameValueCollection queryColl = System.Web.HttpUtility.ParseQueryString(query);

                StringBuilder builder = new StringBuilder();
                builder.AppendLine("{");

                foreach (string key in queryColl)
				{
                    builder.AppendLine(key + ":"  + queryColl[key] + ",");
				}
                builder.AppendLine("}");
                input = builder.ToString();
            }
            else
            {
                input = req.ReadAsString();
            }

            return input.Replace("\r\n", "");
        }
	}
}
