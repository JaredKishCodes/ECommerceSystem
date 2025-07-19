using OrderService.Application;
using OrderService.Infrastructure;
using ProductService.Grpc;

namespace OrderService.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddOrderServiceAPI(this IServiceCollection services, IConfiguration config)
        {
            services.AddOrderServiceApplication()
                .AddOrderServiceInfrastructure(config);

            services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(o =>
            {
                o.Address = new Uri("http://productservice"); 
            });
            return services;
        }
    }
}
