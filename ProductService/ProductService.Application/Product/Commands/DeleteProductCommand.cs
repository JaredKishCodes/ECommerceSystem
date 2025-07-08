
using MediatR;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;

namespace ProductService.Application.Product.Commands
{
    public record DeleteProductCommand(Guid id) : IRequest<bool>;
    public class DeleteProductCommandHandler(IProductService _productService)
        : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            return await _productService.DeleteProductAsync(request.id);
        }
    }
}
