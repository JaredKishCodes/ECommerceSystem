using Grpc.Core;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;
using ProductService.Grpc;
using Microsoft.Extensions.Logging;
using ProductService.Domain.Interface;

namespace ProductService.Application.Services
{
    public class ProductGrpcService : ProductGrpc.ProductGrpcBase
    {
        private readonly IProductService _productService;
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(IProductService productService, ILogger<ProductGrpcService> logger, IProductRepository productRepository)
        {
            _productService = productService;
            _logger = logger;
            _productRepository  = productRepository;
        }

        public override async Task<ProductReply> GetProductById(ProductRequest request, ServerCallContext context)
        {   
            
            var guid = Guid.Parse(request.Id);
            if (guid == Guid.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid product ID format."));
            }

            var product = await _productService.GetProductByIdAsync(guid);

            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID '{request.Id}' not found."));
            }

            _logger.LogInformation("Logging id: {Id}, product: {Name}", request.Id, product.Name);

            return new ProductReply
            {
                Id = product.Id.ToString(),
                Name = product.Name,
                Description = product.Description,
                AvailableStock = product.AvailableStock,
                Price = product.Price
            };
        }

        public override async Task<ReduceStockReply> ReduceStock(ReduceStockRequest request, ServerCallContext context)
        {
            var guid  = Guid.Parse(request.Id);
            if (guid == Guid.Empty)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid product ID format."));
            }
            if (request.Quantity <= 0)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Quantity must be greater than zero."));
            }
            var product = await _productRepository.ReduceStockAsync(guid, request.Quantity);
            if (product == null)
            {
                throw new RpcException(new Status(StatusCode.NotFound, $"Product with ID '{guid}' not found or insufficient stock."));
            }
            return new ReduceStockReply
            {
                Success = true
            };
        }
    }
}
