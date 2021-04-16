using Newtonsoft.Json;

namespace RecentOrderAPI.ViewModels
{
    public class RecentOrderGetDto
    {
        [JsonProperty("customer")]
        public CustomerDto Customer { get; set; }

        [JsonProperty("order")]
        public OrderDto Order { get; set; }
    }
}
