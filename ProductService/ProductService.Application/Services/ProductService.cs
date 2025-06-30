
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;
using ProductService.Domain.Entities;
using ProductService.Domain.Interface;

namespace ProductService.Application.Services
{
    public class ProductService(IProductRepository productRepository, ILogger<ProductService> _logger,IMapper _mapper): IProductService
    {
        public async Task<ProductResponse> AddProductAsync(CreateProductDto createProductDto)
        {
            _logger.LogInformation("Adding a new product with name: {ProductName}", createProductDto.Name);

            bool productExist = await productRepository.ProductNameExistsAsync(createProductDto.Name);

            if (productExist) {

                throw new Exception("Product with the same name already exists.");
            }

            var product = _mapper.Map<Product>(createProductDto);
            product.Id = Guid.NewGuid();

            await productRepository.AddProductAsync(product);

            _logger.LogInformation("Product with ID: {ProductId} added successfully", product.Id);

            return _mapper.Map<ProductResponse>(product);
        }

        public Task<bool> DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> UpdateProductAsync(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
