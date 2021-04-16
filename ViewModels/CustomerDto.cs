﻿using Newtonsoft.Json;

namespace RecentOrderAPI.ViewModels
{
    public class CustomerDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }
    }
}
