using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using MSSQLServerMonitoring.Application;
using MSSQLServerMonitoring.Connector;
using MSSQLServerMonitoring.Hangfire;
using MSSQLServerMonitoring.Infrastructure.Data;
using MSSQLServerMonitoring.Infrastructure.Procedure;
using MSSQLServerMonitoring.Infrastructure.Startup;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MSSQLServerMonitoring.WebApp
{
    public class Startup : IBaseStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            AddServices(services);

            return services.BuildServiceProvider();
        }

        public virtual void AddServices(IServiceCollection services)
        {
            services.AddMvc();
            ConfigureDatabase(services);
            services.AddFeatureManagement();
            services
                .AddBaseServices()
                .AddApplication()
                //.AddVersioning(1, 0)
                .AddWrapper()
                .AddMSSQLServerConnector(new ConfigureMSSQLServerConnectorComponent { BaseApiUrl = Configuration.GetConnectionString("MonitorConnection") })
                .AddAdupter()
                .AddEventBuffer()
                .AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });
            services.Configure<RouteOptions>(options =>
                {
                    options.LowercaseUrls = true;
                    options.LowercaseQueryStrings = true;
                    options.AppendTrailingSlash = true;
                });
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPageIfDev();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddDatabase<ExampleContext>(Configuration.GetConnectionString("ExampleConnection"));
        }
    }
}
