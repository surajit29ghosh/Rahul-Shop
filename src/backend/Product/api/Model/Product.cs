using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Products.Api.Model
{
    public partial class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
		public string Image { get; set; }
		public int AvailableQuantity { get; set; }
		public decimal? Price { get; set; }
		public Guid CategoryId { get; set; }
		public virtual Category Category { get; set; }
	}
}
