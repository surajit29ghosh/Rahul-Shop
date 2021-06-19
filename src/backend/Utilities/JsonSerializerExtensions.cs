using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace eShop.Utilities
{
	public static class JsonSerializerExtensions
	{
		public static T CaseInsensitiveDeseialize<T>(string s)
		{
			return JsonSerializer.Deserialize<T>(s, new JsonSerializerOptions { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true });
		}
	}
}
