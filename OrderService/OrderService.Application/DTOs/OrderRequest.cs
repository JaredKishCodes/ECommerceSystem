

namespace OrderService.Application.DTOs
{
    public class OrderRequest
    {
        
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderedAt { get; set; }
    }
}
