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
                .ConfigureServices((hosting, services) =>
				{
                    string dbConnection = hosting.Configuration.GetSection("DbConnection").Value;
                    services.AddDbContext<ProductDBContext>(options =>
                    {
                        options.UseSqlServer(dbConnection);
                        //options.UseSqlServer("Server=tcp:rm-shop-db-server.database.windows.net,1433;Initial Catalog=rm-shop-db;Persist Security Info=False;User ID=rm-shop-dba;Password=eStore@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
                    });

                    services.AddScoped<IProductRepository, ProductRepository>();
                    services.AddScoped<ICategoryRepository, CategoryRepository>();
                })
                .Build();

            host.Run();
        }
    }
}