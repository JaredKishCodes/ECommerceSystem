using OrderService.Application;
using OrderService.Infrastructure;
using ProductService.Grpc;
using System.Net;
using System.Net.Http;
using Grpc.Net.Client;


namespace OrderService.API
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddOrderServiceAPI(this IServiceCollection services, IConfiguration config)
        {
            services.AddOrderServiceApplication()
                .AddOrderServiceInfrastructure(config);

            services.AddGrpcClient<ProductGrpc.ProductGrpcClient>(options =>
            {
                options.Address = new Uri("http://productservice:5002");
            });



            return services; 
        }
    }
}
