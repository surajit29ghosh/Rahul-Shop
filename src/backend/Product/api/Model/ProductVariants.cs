using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Products.Api.Model
{
	public class ProductVariant
	{
		public Guid VariantId { get; set; }
		public Guid ProductId { get; set; }
		public string VariantType { get; set; }
		public string VariantName { get; set; }
		public decimal? VariantPrice { get; set; }
		public bool VariantActive { get; set; }
		public virtual Product Product { get; set; }
	}
}
