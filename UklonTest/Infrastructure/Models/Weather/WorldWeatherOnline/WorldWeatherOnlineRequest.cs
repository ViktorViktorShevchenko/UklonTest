using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UklonTest.Infrastructure.Attributes;

namespace UklonTest.Infrastructure.Models.Weather.WorldWeatherOnline
{
    public class WorldWeatherOnlineRequest
    {
        [RequestParameter("q")]
        public string City { get; set; }

        [RequestParameter("key")]
        public string Key { get; set; }

        [RequestParameter("format")]
        public string Format { get; set; } = "json";
    }
}
