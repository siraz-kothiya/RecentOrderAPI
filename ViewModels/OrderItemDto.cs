using Newtonsoft.Json;

namespace RecentOrderAPI.ViewModels
{
    public class OrderItemDto
    {
            [JsonProperty("product")]
            public string Product { get; set; }

            [JsonProperty("quantity")]
            public int? Quantity { get; set; }

            [JsonProperty("priceEach")]
            public string PriceEach { get; set; }
    }

}
