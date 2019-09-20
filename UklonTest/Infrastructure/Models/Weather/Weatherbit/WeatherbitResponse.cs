using Newtonsoft.Json;
using System.Collections.Generic;

namespace UklonTest.Infrastructure.Models.Weather.Weatherbit
{
    public class WeatherbitResponse
    {
        [JsonProperty("data")]
        public List<Data> Data { get; set; }
    }

    public class Data
    {
        [JsonProperty(PropertyName = "temp")]
        public decimal Temperature { get; set; }

        [JsonProperty(PropertyName = "wind_speed")]
        public decimal WindSpeed { get; set; }

        [JsonProperty(PropertyName = "wind_dir")]
        public int WindDirection { get; set; }

        [JsonProperty(PropertyName = "ts")]
        public long EpochTime { get; set; }
    }
}
