using Microsoft.EntityFrameworkCore;
using RecentOrderAPI.Context;
using RecentOrderAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace RecentOrderAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SSE_TestContext _context;
        
        public OrderRepository(SSE_TestContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context)); 
        }
        public async Task<Order> GetCustomerMostRecentOrder(string user,string customerId)
        {
            return await _context.Orders.Include(o => o.Orderitems).ThenInclude(oi => oi.Product)
                                                 .Where(o => o.Customerid == customerId)
                                                 .OrderByDescending(o => o.Orderdate)
                                                 .FirstOrDefaultAsync();
        }
    }
}
