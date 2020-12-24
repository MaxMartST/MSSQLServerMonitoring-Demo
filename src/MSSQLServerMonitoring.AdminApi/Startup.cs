using System;
using MSSQLServerMonitoring.Application;
using MSSQLServerMonitoring.Infrastructure.Data;
using MSSQLServerMonitoring.Infrastructure.Startup;
using MSSQLServerMonitoring.Infrastructure.Web;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using MSSQLServerMonitoring.Connector;

namespace MSSQLServerMonitoring.AdminApi
{
    public class Startup : IBaseStartup
    {
        private readonly IHostingEnvironment _env;
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration, IHostingEnvironment env )
        {
            _env = env;
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices( IServiceCollection services )
        {
            AddServices( services );

            return services.BuildServiceProvider();
        }

        public virtual void AddServices( IServiceCollection services )
        {
            ConfigureDatabase( services );
            services.AddFeatureManagement();
            services
                .AddBaseServices()
                .AddApplication()
                .AddVersioning( 1, 0 )
                .AddMSSQLServerConnector(Configuration.GetSection("MonitorConnection").Get<ConfigureMSSQLServerConnectorComponent>())
                .AddDefaultMvc("MSSQLServerMonitoring.AdminApi")
                .AddJsonOptions( options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters.Add( new StringEnumConverter() );
                } );
        }

        public virtual void Configure( IApplicationBuilder app )
        {
            app.UseMvcWithDefaultRoute();
        }

        public virtual void ConfigureDatabase( IServiceCollection services )
        {
            services.AddDatabase<ExampleContext>( Configuration.GetConnectionString( "ExampleConnection" ) );
        }
    }
}
