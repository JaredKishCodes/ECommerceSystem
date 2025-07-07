
using ProductEntity = ProductService.Domain.Entities;


namespace ProductService.Application.Product.Commands
{
   public record UpdateProductCommand(ProductEntity Product)
    public class UpdateProductCommandHandler()
    {
    }
}
