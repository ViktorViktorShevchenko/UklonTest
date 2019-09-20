using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;
using UklonTest.Models;

namespace UklonTest.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IHostingEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, IHostingEnvironment env)
        {
            this.env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json; charset=utf-8";

            var ex = context.Features.Get<IExceptionHandlerFeature>();
            if (ex != null)
            {
                var err = BuildError(ex.Error);

                await context.Response
                    .WriteAsync(err, Encoding.UTF8)
                    .ConfigureAwait(false);
            }
        }

        private string BuildError(Exception ex)
        {
            var error = new ErrorResponse()
            {
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace
                
            };

            return JsonConvert.SerializeObject(error);
        }
    }
}
