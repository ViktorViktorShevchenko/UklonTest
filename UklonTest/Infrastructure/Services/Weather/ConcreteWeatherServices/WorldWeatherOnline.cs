using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UklonTest.Infrastructure.Models.Weather;
using UklonTest.Infrastructure.Models.Weather.WorldWeatherOnline;
using UklonTest.Infrastructure.Services.WebProvider;
using UklonTest.Infrastructure.Weather.Models;
using UklonTest.Models;

namespace UklonTest.Infrastructure.Services.Weather.ConcreteWeatherServices
{
    public class WorldWeatherOnline : WeatherService
    {
        public WorldWeatherOnline(IWebProvider webProvider, IOptions<WorldWeatherOnlineOptions> weatherServiceOptions) 
            : base(webProvider, weatherServiceOptions) { }

        public override async Task<WeatherResponse> GetWeather(string city, string country)
        {
            var request = new WorldWeatherOnlineRequest()
            {
                City = city,
                Key = weatherServiceOptions.Key
            };

            var response = await webProvider
                .RequestGetAsync<WorldWeatherOnlineResponse>(weatherServiceOptions.Url, request);

            var currentCondition = response?.Data?.CurrentCondition?.FirstOrDefault();

            if (currentCondition == null)
                throw new Exception(ErrorCodes.CITY_NOT_FOUND.ToString());

            return new WeatherResponse(currentCondition.Temperature, currentCondition.WindSpeed,
                currentCondition.WindDirection, currentCondition.ObservationTime, 
                WeatherServicesList.WorldWeatherOnline);
        }
    }
}
