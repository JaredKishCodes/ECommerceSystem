using OrderService.Application;
using OrderService.Infrastructure;
using ProductService.Grpc;
using System.Net;
using System.Net.Http;
using Grpc.Net.Client;
using System.Net.Security;


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
            })
            .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
            {
                SslOptions = new SslClientAuthenticationOptions
                {
                    RemoteCertificateValidationCallback = (sender, certificate, chain, errors) => true
                },
                EnableMultipleHttp2Connections = true
            });




            return services; 
        }
    }
}
