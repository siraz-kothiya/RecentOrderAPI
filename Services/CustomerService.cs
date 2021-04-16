using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RecentOrderAPI.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecentOrderAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IConfiguration _configuration;
        public CustomerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<CustomerDetailsDto> GetCustomerDetails(string email)
        {
            using (var httpClient = new HttpClient())
            {
                var customerAPIUrl = _configuration["CustomerAPI:CustomerAPIUrl"];
                var customerAPIKey = _configuration["CustomerAPI:CustomerAPIKey"];
                var apiParam = string.Format("?code={0}&email={1}", customerAPIKey, email);
                string url = string.Concat(customerAPIUrl, apiParam);
                using (var response = await httpClient.GetAsync(url))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CustomerDetailsDto>(apiResponse);
                }
            }
        }
    }
}
