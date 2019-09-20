using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UklonTest.Infrastructure.Models.Weather;
using UklonTest.Infrastructure.Models.Weather.Weatherbit;
using UklonTest.Infrastructure.Services.WebProvider;
using UklonTest.Infrastructure.Weather.Models;
using UklonTest.Models;

namespace UklonTest.Infrastructure.Services.Weather.ConcreteWeatherServices
{
    public class Weatherbit : WeatherService
    {
        public Weatherbit(IWebProvider webProvider, IOptions<WeatherbitOptions> weatherServiceOptions) 
            : base(webProvider, weatherServiceOptions) { }

        public override async Task<WeatherResponse> GetWeather(string city, string country)
        {
            var request = new WeatherbitRequest()
            {
                Key = weatherServiceOptions.Key,
                City = city,
                Country = country
            };

            var response = await webProvider
                .RequestGetAsync<WeatherbitResponse>(weatherServiceOptions.Url, request);

            var data = response?.Data?.FirstOrDefault();

            if (data == null)
                throw new Exception(ErrorCodes.CITY_NOT_FOUND.ToString());

            return new WeatherResponse(data.Temperature, data.WindSpeed, data.WindDirection,
                DateTimeOffset.FromUnixTimeSeconds(data.EpochTime).DateTime, WeatherServicesList.Weatherbit);
        }
    }
}
