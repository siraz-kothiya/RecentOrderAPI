using Microsoft.AspNetCore.Mvc;
using RecentOrderAPI.ViewModels;
using System.Threading.Tasks;

namespace RecentOrderAPI.Services
{
    public interface ICustomerService
    {
        Task<CustomerDetailsDto> GetCustomerDetails(string email);
    }
}
