using eShop.Products.Api.Infrastructure;
using eShop.Products.Api.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Products.Api.Repository
{
	public class ProductRepository : IProductRepository
	{
		private ProductDBContext productDBContext;
		public ProductRepository(ProductDBContext context)
		{
			productDBContext = context;
		}

		public IEnumerable<Product> GetAllProducts(SearchProductModel page)
		{
			var query = productDBContext.Products.AsQueryable();

			query = query.Include(p => p.Category);


			if (!string.IsNullOrEmpty(page.CategoryTerm))
				query = query.Where(p => p.Category.CategoryName.Contains(page.CategoryTerm));

			if (!string.IsNullOrEmpty(page.SearchTerm))
				query = query.Where(p => p.ProductName.Contains(page.SearchTerm));

			if (page.PageNo > 1)
				query = query.Skip(page.PageNo * page.PageSize);

			query = query.Take(page.PageSize);

			var data = query.OrderBy(o => o.ProductName).ToList();

			return data;
		}

		public IEnumerable<Product> SearchProducts(SearchProductModel page)
		{
			var query = productDBContext.Products
										.Include(p => p.Category)
										.Where(p => p.ProductName.Contains(page.SearchTerm));

			if (page.PageNo > 1)
				query.Skip(page.PageNo * page.PageSize);

			var data = query.Take(page.PageSize).OrderBy(o => o.ProductName).ToList();

			return data;
		}

		public Product GetProduct(string productId)
		{
			return productDBContext.Products.Where(s => s.ProductId == Guid.Parse(productId))
									.Include(p => p.Category)
									.FirstOrDefault();
		}
	}

	public interface IProductRepository
	{
		IEnumerable<Product> GetAllProducts(SearchProductModel page);
		IEnumerable<Product> SearchProducts(SearchProductModel page);
		Product GetProduct(string productId);
	}
}
