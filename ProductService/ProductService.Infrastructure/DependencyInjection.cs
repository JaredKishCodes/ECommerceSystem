
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Interface;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repository;

namespace ProductService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.AddDbContext<ProductDbContext>(options =>
            {
                services.AddScoped<IProductRepository, ProductRepository>();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            return services;
        }
    }
}
