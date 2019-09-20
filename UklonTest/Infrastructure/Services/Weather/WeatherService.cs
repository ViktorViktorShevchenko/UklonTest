using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using UklonTest.Infrastructure.Services.WebProvider;
using UklonTest.Infrastructure.Weather.Models;

namespace UklonTest.Infrastructure.Services
{
    public abstract class WeatherService
    {
        protected readonly IWebProvider webProvider;
        protected readonly WeatherOptions weatherServiceOptions;

        public WeatherService(IWebProvider webProvider, IOptions<WeatherOptions> weatherServiceOptions)
        {
            this.webProvider = webProvider;
            this.weatherServiceOptions = weatherServiceOptions.Value;
        }

        public abstract Task<WeatherResponse> GetWeather(string city, string country);

    }
}
