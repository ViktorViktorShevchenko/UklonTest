using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using UklonTest.Infrastructure.Models.Weather.OpenWeather;
using UklonTest.Infrastructure.Models.Weather.WorldWeatherOnline;
using UklonTest.Infrastructure.Services;
using UklonTest.Infrastructure.Services.File;
using UklonTest.Infrastructure.Services.Weather.ConcreteWeatherServices;
using UklonTest.Infrastructure.Services.WebProvider;
using UklonTest.Infrastructure.Weather.Models;
using UklonTest.Middleware;
using FileOptions = UklonTest.Infrastructure.Models.FileOptions;

namespace UklonTest
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var weatherbitSection = Configuration.GetSection("Weatherbit");
            services.Configure<WeatherbitOptions>(weatherbitSection);

            var openWeather = Configuration.GetSection("OpenWeather");
            services.Configure<OpenWeatherOptions>(openWeather);

            var worldWeather = Configuration.GetSection("WorldWeatherOnline");
            services.Configure<WorldWeatherOnlineOptions>(worldWeather);

            var file = Configuration.GetSection("File");
            services.Configure<FileOptions>(file);

            services.AddHttpClient();
            services.AddSingleton<IWebProvider, WebProvider>();
            services.AddSingleton<WeatherService, Weatherbit>();
            services.AddSingleton<WeatherService, OpenWeather>();
            services.AddSingleton<WeatherService, WorldWeatherOnline>();
            services.AddTransient<IFileService, FileService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "Uklon Test API", Version = "1.0" });
                var xmlPath = Path.Combine(AppContext.BaseDirectory, "UklonTest.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(options =>
                options.UseMiddleware<ExceptionMiddleware>());

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Uklon Test API V1.0");
                c.RoutePrefix = string.Empty;
            });

            app.UseMvc();
        }
    }
}
