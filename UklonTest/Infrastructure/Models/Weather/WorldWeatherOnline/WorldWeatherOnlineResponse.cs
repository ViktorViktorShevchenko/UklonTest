using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UklonTest.Infrastructure.Models.Weather.WorldWeatherOnline
{
    public class WorldWeatherOnlineResponse
    {
        [JsonProperty(PropertyName = "data")]
        public Data Data { get; set; }
    }

    public class Data
    {
        [JsonProperty(PropertyName = "current_condition")]
        public List<CurrentCodition> CurrentCondition { get; set; }
    }

    public class CurrentCodition
    {
        [JsonProperty(PropertyName = "observation_time")]
        public DateTime ObservationTime { get; set; }

        [JsonProperty(PropertyName ="temp_C")]
        public int Temperature { get; set; }

        [JsonProperty(PropertyName = "windspeedKmph")]
        public int WindSpeed { get; set; }

        [JsonProperty(PropertyName = "winddirDegree")]
        public int WindDirection { get; set; }
    }
}
