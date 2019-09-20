using System.Threading.Tasks;

namespace UklonTest.Infrastructure.Services.WebProvider
{
    public interface IWebProvider
    {
        Task<T> RequestGetAsync<T>(string baseUrl, object dataForRequest);
    }
}
