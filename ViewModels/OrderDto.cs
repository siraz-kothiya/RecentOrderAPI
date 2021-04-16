using Newtonsoft.Json;
using System.Collections.Generic;

namespace RecentOrderAPI.ViewModels
{
    public class OrderDto
    {
        [JsonProperty("orderNumber")]
        public long OrderNumber { get; set; }

        [JsonProperty("orderDate")]
        public string OrderDate { get; set; }

        [JsonProperty("deliveryAddress")]
        public string DeliveryAddress { get; set; }

        [JsonProperty("orderItems")]
        public List<OrderItemDto> OrderItems { get; set; }

        [JsonProperty("deliveryExpected")]
        public string DeliveryExpected { get; set; }
    }
}
