using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Interface;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Repository;

namespace OrderService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddOrderServiceInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
