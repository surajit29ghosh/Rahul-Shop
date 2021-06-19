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
	public class CategoryRepository: ICategoryRepository
	{
		private ProductDBContext productDBContext;
		public CategoryRepository(ProductDBContext context)
		{
			productDBContext = context;
		}

		public IEnumerable<Category> GetCategoriesWithProducts(PageModel page)
		{
			var query = productDBContext.Categories.Include(c => c.Products);


			if (page.PageNo > 0)
				query.Skip(page.PageNo * page.PageSize);

			if (page.PageSize > 0)
				query.Take(page.PageSize);

			var data = query.OrderBy(o => o.CategoryName).ToList();

			return data;
		}

		public IEnumerable<Category> SearchCategories(SearchProductModel page)
		{
			var query = productDBContext.Categories
										.Include(p => p.Products)
										.Where(p => p.CategoryName.Contains(page.SearchTerm));

			if (page.PageNo > 0)
				query.Skip(page.PageNo * page.PageSize);

			if (page.PageSize > 0)
				query.Take(page.PageSize);

			var data = query.OrderBy(o => o.CategoryName).ToList();

			return data;
		}

		public Category GetCategory(string categoryId)
		{
			return productDBContext.Categories.Where(s => s.CategoryId == Guid.Parse(categoryId))
									.Include(p => p.Products)
									.FirstOrDefault();
		}
	}

	public interface ICategoryRepository
	{
		IEnumerable<Category> GetCategoriesWithProducts(PageModel page);
		IEnumerable<Category> SearchCategories(SearchProductModel page);
		Category GetCategory(string categoryId);

	}
}
