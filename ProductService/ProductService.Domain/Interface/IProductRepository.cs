
using ProductService.Domain.Entities;

namespace ProductService.Domain.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(Guid id);
        Task<Product> ReduceStockAsync(Guid productId, int quantity);
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync( Product product);

        Task<bool> ProductNameExistsAsync(string name);
        Task<bool> DeleteProductAsync(Guid id);
    }
}
