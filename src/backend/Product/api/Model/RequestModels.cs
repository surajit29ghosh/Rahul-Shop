using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Products.Api.Model
{
	public class PageModel
	{
		public int PageNo { get; set; }
		public int PageSize { get; set; }
	}

	public class SearchProductModel: PageModel
	{
		public string SearchTerm { get; set; }
		public string CategoryTerm { get; set; }
	}

	public class ProductRequest
	{
		public string ProductId { get; set; }
	}
}
