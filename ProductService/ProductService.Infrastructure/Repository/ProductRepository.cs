
using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Entities;
using ProductService.Domain.Interface;
using ProductService.Infrastructure.Data;

namespace ProductService.Infrastructure.Repository
{
    public class ProductRepository(ProductDbContext _dbContext) : IProductRepository
    {
        public async Task AddProductAsync(Product product)
        {
          await _dbContext.Products.AddAsync(product);
          await  _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
          var product =  await _dbContext.Products.FindAsync(id);
            if (product != null)
            { 
                _dbContext.Products.Remove(product);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;   
            }
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
          return await _dbContext.Products.ToListAsync();    
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> ProductNameExistsAsync(string name)
        {
            return await _dbContext.Products.AnyAsync(p => p.Name.ToLower() == name.ToLower());
        }

        public async Task UpdateProductAsync( Product product)
        {
             _dbContext.Products.Update(product); 
            await _dbContext.SaveChangesAsync();
        }
    }
}
