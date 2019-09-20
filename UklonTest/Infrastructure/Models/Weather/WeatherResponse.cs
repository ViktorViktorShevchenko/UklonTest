using Newtonsoft.Json;
using System;
using UklonTest.Infrastructure.Models.Weather;

namespace UklonTest.Infrastructure.Weather.Models
{
    public class WeatherResponse
    {
        [JsonProperty(PropertyName = "temperature")]
        public decimal Temperature { get; set; }

        [JsonProperty(PropertyName = "windSpeed")]
        public decimal WindSpeed { get; set; }

        [JsonProperty(PropertyName = "windDir")]
        public int WindDirection { get; set; }

        [JsonProperty(PropertyName = "observationTime")]
        public DateTime ObservationTime { get; set; }

        [JsonIgnore]
        public WeatherServicesList WheatherService { get; set; }

        public WeatherResponse(decimal temperature, decimal windSpeed, int windDirection, 
            DateTime observationTime, WeatherServicesList weatherService)
        {
            this.Temperature = temperature;
            this.WindSpeed = windSpeed;
            this.WindDirection = windDirection;
            this.ObservationTime = observationTime;
            this.WheatherService = weatherService;
        }
    }
}
