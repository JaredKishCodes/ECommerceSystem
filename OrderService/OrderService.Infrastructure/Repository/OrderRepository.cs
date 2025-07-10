

using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.Interface;
using OrderService.Infrastructure.Persistence;

namespace OrderService.Infrastructure.Repository
{
    public class OrderRepository(OrderDbContext _dbContext) : IOrderRepository
    {
        public async Task AddOrderAsync(Order order)
        {
           await _dbContext.Orders.AddAsync(order);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteOrderAsync(Guid orderId)
        {
            var order = await _dbContext.Orders.FindAsync(orderId);
            if(order is not null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
         return await  _dbContext.Orders.ToListAsync();

        }

        public async Task<Order?> GetOrderByIdAsync(Guid orderId)
        {
            return await _dbContext.Orders
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task<Order?> UpdateOrdersync( Order order)
        {
             _dbContext.Orders.Update(order); 
            await _dbContext.SaveChangesAsync();
            // Return the updated order
            return order;
        }
    }
}
