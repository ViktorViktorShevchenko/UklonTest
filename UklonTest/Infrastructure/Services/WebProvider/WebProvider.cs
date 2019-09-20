using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UklonTest.Infrastructure.Attributes;
using UklonTest.Models;

namespace UklonTest.Infrastructure.Services.WebProvider
{
    public class WebProvider : IWebProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WebProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<T> RequestGetAsync<T>(string baseUrl, object dataForRequest)
        {
            string url = baseUrl + RequestFormat(dataForRequest);

            var client = httpClientFactory.CreateClient();

            var httpResponseMessage = await client.GetAsync(url);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }

        private string RequestFormat(object data)
        {
            if (data == null)
                throw new Exception(ErrorCodes.REQUEST_FORMAT_NULL_ERROR.ToString());

            var type = data.GetType();
            var properties = type.GetProperties();
            var builder = new StringBuilder();

            foreach (var property in properties)
            {
                var attribute = (RequestParameterAttribute)property.GetCustomAttributes(typeof(RequestParameterAttribute), false)
                    .SingleOrDefault();

                if (attribute != null)
                {
                    var propertyValue = property.GetValue(data)?.ToString();

                    if (!string.IsNullOrEmpty(propertyValue))
                        builder.Append($"{attribute.ParameterName}={propertyValue}&");
                }
            }

            string result = builder.ToString();

            return result;
        }
    }
}
