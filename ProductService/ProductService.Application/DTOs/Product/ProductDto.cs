
namespace ProductService.Application.DTOs.Product
{
    public class ProductDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int AvailableStock { get; set; }
    }
}
