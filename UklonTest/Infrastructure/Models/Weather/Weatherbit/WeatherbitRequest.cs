using UklonTest.Infrastructure.Attributes;

namespace UklonTest.Infrastructure.Models.Weather.Weatherbit
{
    public class WeatherbitRequest
    {
        [RequestParameter("city")]
        public string City { get; set; }

        [RequestParameter("country")]
        public string Country { get; set; }

        [RequestParameter("key")]
        public string Key { get; set; }
    }
}
