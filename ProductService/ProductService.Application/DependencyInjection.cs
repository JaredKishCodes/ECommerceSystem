

using Microsoft.Extensions.DependencyInjection;

using ProductService.Application.Interfaces;
using ProductService.Application.Mapping;
using ProductService.Application.Services;


namespace ProductService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
            services.AddAutoMapper(typeof(ProductProfile));

            services.AddScoped<IProductService, ProductApplicationService>();
            return services;
        }
    }
}
