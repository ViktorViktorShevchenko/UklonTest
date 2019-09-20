using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UklonTest.Infrastructure.Services;
using UklonTest.Infrastructure.Weather.Models;
using UklonTest.Models;

namespace UklonTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]

    public class WeatherController : ControllerBase
    {
        private readonly IEnumerable<WeatherService> weatherServices;
        private readonly IFileService file;

        public WeatherController(IEnumerable<WeatherService> weatherServices, IFileService file)
        {
            this.weatherServices = weatherServices;
            this.file = file;
        }

        [HttpGet]
        [ProducesResponseType(typeof(WeatherResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWeather([Required]string city, string country)
        {
            var tasks = new List<Task<WeatherResponse>>();

            foreach (var weatherService in weatherServices)
            {
                tasks.Add(weatherService.GetWeather(city, country));
            }

            Task<WeatherResponse> response;
            do
            {
                response = await Task.WhenAny(tasks);
                tasks.Remove(response);
            } while (response.IsFaulted && tasks.Count > 0);

            if (response.IsFaulted)
                throw new Exception(ErrorCodes.CITY_NOT_FOUND.ToString());

            var weather = await response;

            await file.Write(weather);

            return Ok(weather);
        }
    }
}