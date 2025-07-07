using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductService.Application.DTOs.Product;
using ProductService.Application.Interfaces;
using ProductEntity = ProductService.Domain.Entities.Product;

using ProductService.Domain.Interface;


namespace ProductService.Application.Services
{
    public class ProductApplicationService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductApplicationService> _logger;
        private readonly IMapper _mapper;

        public ProductApplicationService(
            IProductRepository productRepository,
            ILogger<ProductApplicationService> logger,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ProductResponse> AddProductAsync(CreateProductDto createProductDto)
        {
            _logger.LogInformation("Adding a new product with name: {ProductName}", createProductDto.Name);

            bool productExist = await _productRepository.ProductNameExistsAsync(createProductDto.Name);
            if (productExist)
            {
                throw new Exception("Product with the same name already exists.");
            }

            var product = _mapper.Map<ProductEntity>(createProductDto);
            product.Id = Guid.NewGuid();

            await _productRepository.AddProductAsync(product);

            _logger.LogInformation("Product with ID: {ProductId} added successfully", product.Id);

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Product ID cannot be empty.", nameof(id));
            }

            _logger.LogInformation("Deleting product with ID: {ProductId}", id);
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            _logger.LogInformation("Retrieving all products");

            var products = await _productRepository.GetAllProductsAsync();
            if (products == null || !products.Any())
            {
                throw new Exception("No products found.");
            }

            return products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                AvailableStock = p.AvailableStock
            });
        }

        public async Task<ProductResponse> GetProductByIdAsync(Guid id)
        {
            _logger.LogInformation("Retrieving product with ID: {ProductId}", id);

            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id), "ID can't be empty.");
            }

            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                throw new Exception($"Product with ID {id} not found.");
            }

            return _mapper.Map<ProductResponse>(product);
        }

        public async Task<ProductResponse> UpdateProductAsync(ProductDto product)
        {
            _logger.LogInformation("Updating product: {ProductId}", product.Id);

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product), "Product cannot be null.");
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(product.Id);
            if (existingProduct == null)
            {
                throw new Exception($"Product with ID {product.Id} not found.");
            }

            _mapper.Map(product, existingProduct);
            await _productRepository.UpdateProductAsync(existingProduct);

            return _mapper.Map<ProductResponse>(existingProduct);
        }
    }
}
