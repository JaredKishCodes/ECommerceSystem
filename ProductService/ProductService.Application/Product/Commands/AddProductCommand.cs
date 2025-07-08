
using MediatR;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Product.Commands
{   
    public record AddProductCommand(CreateProductDto ProductDto) : IRequest<ProductResponse>;
    public class AddProductCommandHandler(IProductService _productService)
            : IRequestHandler<AddProductCommand, ProductResponse>
    {
        public async Task<ProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.AddProductAsync(request.ProductDto);
        }
    }
}
