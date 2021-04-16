using RecentOrderAPI.Models;
using System.Threading.Tasks;

namespace RecentOrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> GetCustomerMostRecentOrder(string user,string customerId);
    }
}
