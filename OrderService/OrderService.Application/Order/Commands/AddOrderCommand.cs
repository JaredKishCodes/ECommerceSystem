using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grpc.Core;
using MediatR;
using OrderService.Application.DTOs;
using OrderService.Domain.Interface;
using ProductService.Grpc;

namespace OrderService.Application.Order.Commands
{
    public class AddOrderCommand( Guid productId, int quantity) : IRequest<OrderResponse>
    {      
        public Guid ProductId { get; set; } = productId;
        public int Quantity { get; } = quantity;
    }


    public class AddOrderCommandHandler(ProductGrpc.ProductGrpcClient productClient,IOrderRepository orderRepository)
        : IRequestHandler<AddOrderCommand, OrderResponse>
    {
        public async Task<OrderResponse> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            if (request.ProductId == Guid.Empty)
            {
                throw new ArgumentException("Order ID cannot be empty.", nameof(request.ProductId));
            }

            var product = await  productClient.GetProductByIdAsync( new ProductRequest { Id = request.ProductId.ToString() }, cancellationToken: cancellationToken);

            if(product == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Product not found"));

            if (product.AvailableStock < request.Quantity)
            {
                throw new InvalidOperationException("Insufficient stock for the product.");
            }

            await productClient.ReduceStockAsync(new ReduceStockRequest
            {
                Id = product.Id,
                Quantity = request.Quantity
            }, cancellationToken: cancellationToken);

            await orderRepository.AddOrderAsync(new Domain.Entities.Order
            {
                
                ProductId = Guid.Parse(product.Id),
                Quantity = request.Quantity,
                OrderedAt = DateTime.UtcNow
            });
            return new OrderResponse
            {
                
                ProductId = Guid.Parse(product.Id),
                Quantity = request.Quantity,
                OrderedAt = DateTime.UtcNow
            };
        }
    }
}
