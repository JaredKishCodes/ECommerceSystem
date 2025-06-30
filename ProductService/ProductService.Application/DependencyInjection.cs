

using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            // Register application services here
            // Example: services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
