using Newtonsoft.Json;

namespace UklonTest.Infrastructure.Models.Weather.OpenWeather
{
    public class OpenWeatherResponse
    {
        [JsonProperty(PropertyName = "main")]
        public Main Main { get; set; }

        [JsonProperty(PropertyName = "wind")]
        public Wind Wind { get; set; }

        [JsonProperty(PropertyName = "dt")]
        public long EpochTime { get; set; }
    }

    public class Main
    {
        [JsonProperty(PropertyName = "temp")]
        public decimal Temperature { get; set; }
    }

    public class Wind
    {
        [JsonProperty(PropertyName = "speed")]
        public decimal Speed { get; set; }

        [JsonProperty(PropertyName = "deg")]
        public decimal Direction { get; set; }
    }
}
