
using ProductService.Application.DTOs.Product;
using ProductService.Domain.Entities;

namespace ProductService.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductResponse> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse> AddProductAsync(CreateProductDto product);
        Task<ProductResponse> UpdateProductAsync(ProductDto product);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
