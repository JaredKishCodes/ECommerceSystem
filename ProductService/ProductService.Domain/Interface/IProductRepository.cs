
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync( Product product);

        Task<bool> ProductNameExistsAsync(string name);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
