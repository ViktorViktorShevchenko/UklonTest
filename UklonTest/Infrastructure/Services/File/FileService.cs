using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using UklonTest.Infrastructure.Weather.Models;
using FileOptions = UklonTest.Infrastructure.Models.FileOptions;

namespace UklonTest.Infrastructure.Services.File
{
    public class FileService : IFileService
    {
        private readonly FileOptions fileOptions;

        public FileService(IOptions<FileOptions> fileOptions)
        {
            this.fileOptions = fileOptions.Value;
        }

        public async Task Write(WeatherResponse weatherResponse)
        {
            string filePath = fileOptions.Path.Replace("{date}", DateTime.Now.ToShortDateString());

            string text = "Температура:" + weatherResponse.Temperature.ToString() + ", " +
                          "Скорость ветра:" + weatherResponse.WindSpeed.ToString() + ", " +
                          "Направление ветра:" + weatherResponse.WindDirection.ToString() + ", " +
                          "Сервис:" + weatherResponse.WheatherService.ToString();

            using (StreamWriter streamWriter = new StreamWriter(filePath, true, Encoding.UTF8))
            {
                await streamWriter.WriteLineAsync(text);
            }
        }
    }
}
