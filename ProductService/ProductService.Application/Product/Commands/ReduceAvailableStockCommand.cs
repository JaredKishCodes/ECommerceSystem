
using AutoMapper;
using MediatR;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;
using ProductService.Domain.Interface;

namespace ProductService.Application.Product.Commands
{
    public record ReduceAvailableStockCommand(Guid ProductId, int Quantity) : IRequest<ProductResponse>;
    public class ReduceAvailableStockCommandHandler(IMapper _mapper, IProductRepository _productRepository) : IRequestHandler<ReduceAvailableStockCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(ReduceAvailableStockCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.ReduceStockAsync( request.ProductId, request.Quantity);

            if (product == null)
            {
                throw new Exception("Product not found or insufficient stock.");
            }

            return _mapper.Map<ProductResponse>(product);
        }
    }
}
