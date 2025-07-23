using OrderService.Application;
using OrderService.Infrastructure;
using ProductService.Grpc;
using Grpc.Core;
using System.Net.Http;

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
                options.Address = new Uri("http://productservice:5002"); // Use http, not https
            })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                EnableMultipleHttp2Connections = true,
            })
            .ConfigureChannel(options =>
            {
                options.Credentials = ChannelCredentials.Insecure; // Insecure for HTTP
            });

            return services;
        }
    }
}
