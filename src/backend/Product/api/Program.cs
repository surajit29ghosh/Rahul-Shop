using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.DependencyInjection;
using eShop.Products.Api.Infrastructure;
using Microsoft.EntityFrameworkCore;
using eShop.Products.Api.Repository;

namespace api
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
				{
                    services.AddDbContext<ProductDBContext>(options =>
                    {
                        options.UseSqlServer("Server=SurajitG-LTP;Database=eShop;User Id=sa;Password=sa@123;");
                    });

                    services.AddScoped<IProductRepository, ProductRepository>();
                    services.AddScoped<ICategoryRepository, CategoryRepository>();
                })
                .Build();

            host.Run();
        }
    }
}