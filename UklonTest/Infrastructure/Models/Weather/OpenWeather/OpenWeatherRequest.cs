using Newtonsoft.Json;
using UklonTest.Infrastructure.Attributes;

namespace UklonTest.Infrastructure.Models.Weather.OpenWeather
{
    public class OpenWeatherRequest
    {
        [RequestParameter("q")]
        public string City { get; set; }

        [RequestParameter("appid")]
        public string Key { get; set; }

        [RequestParameter("units")]
        public string Units { get; set; } = "metric";
    }
}
