using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UklonTest.Infrastructure.Models.Weather;
using UklonTest.Infrastructure.Models.Weather.OpenWeather;
using UklonTest.Infrastructure.Services.WebProvider;
using UklonTest.Infrastructure.Weather.Models;
using UklonTest.Models;

namespace UklonTest.Infrastructure.Services.Weather.ConcreteWeatherServices
{
    public class OpenWeather : WeatherService
    {
        public OpenWeather(IWebProvider webProvider, IOptions<OpenWeatherOptions> weatherServiceOptions) 
            : base(webProvider, weatherServiceOptions) { }

        public override async Task<WeatherResponse> GetWeather(string city, string country)
        {
            var request = new OpenWeatherRequest()
            {
                City = city,
                Key = weatherServiceOptions.Key
            };

            var response = await webProvider
                .RequestGetAsync<OpenWeatherResponse>(weatherServiceOptions.Url, request);

            if (response == null)
                throw new Exception(ErrorCodes.CITY_NOT_FOUND.ToString());

            return new WeatherResponse(response.Main.Temperature, response.Wind.Speed, (int)response.Wind.Direction,
                DateTimeOffset.FromUnixTimeSeconds(response.EpochTime).DateTime, WeatherServicesList.OpenWeatherMap);
        }
    }
}
