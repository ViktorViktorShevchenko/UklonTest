using System.Threading.Tasks;
using UklonTest.Infrastructure.Weather.Models;

namespace UklonTest.Infrastructure.Services
{
    public interface IFileService
    {
        Task Write(WeatherResponse weatherResponse);
    }
}
