

using OrderService.Domain.Entities;

namespace OrderService.Domain.Interface
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order?> GetOrderByIdAsync(Guid orderId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();

        Task<Order?> UpdateOrdersync( Order order);
        Task<bool> DeleteOrderAsync(Guid orderId);
    }
}
