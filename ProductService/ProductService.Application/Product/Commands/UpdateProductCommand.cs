
using MediatR;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;
using ProductEntity = ProductService.Domain.Entities.Product;


namespace ProductService.Application.Product.Commands
{
    public record UpdateProductCommand(ProductDto Product) : IRequest<ProductResponse>;
    public class UpdateProductCommandHandler(IProductService _productService)
        : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.UpdateProductAsync(request.Product);
        }
    }
}
