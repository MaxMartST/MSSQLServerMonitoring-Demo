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
using MSSQLServerMonitoring.Hangfire;
using Microsoft.AspNetCore.Http;
using MSSQLServerMonitoring.Infrastructure.Procedure;
using Hangfire;
using MSSQLServerMonitoring.HangFire.HangFire;
using MSSQLServerMonitoring.Infrastructure.Data.HangFireModel;
using Hangfire.SqlServer;

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
            AddServices(services);

            JobStorage.Current = new SqlServerStorage(Configuration.GetConnectionString("ExampleConnection"));
            var sp = services.BuildServiceProvider();
            var hangFireService = sp.GetService<IHangFireService>();
            RecurringJob.AddOrUpdate("demo-jod", () => hangFireService.RunDemoTask(), Cron.Minutely);
            //RecurringJob.AddOrUpdate("demo-jod", () => hangFireService.RunSecondDemoTask(), "*/5 * * * * *");

            return services.BuildServiceProvider();
        }

        public virtual void AddServices( IServiceCollection services )
        {
            ConfigureDatabase( services );
            ConfigureHangFire(services );
            services.AddFeatureManagement();
            services
                .AddBaseServices()
                .AddApplication()
                .AddVersioning( 1, 0 )
                
                .AddMSSQLServerConnector( new ConfigureMSSQLServerConnectorComponent { BaseApiUrl = Configuration.GetConnectionString( "MonitorConnection" ) } )
                .AddAdupter()
                .AddEventBuffer()
                .AddDefaultMvc( "MSSQLServerMonitoring.AdminApi" )
                .AddJsonOptions( options =>
                {
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                    options.SerializerSettings.Converters.Add( new StringEnumConverter() );
                } );
        }

        public virtual void Configure( IApplicationBuilder app )
        {
            app.UseMvcWithDefaultRoute();
            app.UseHangfireServer();
        }

        public virtual void ConfigureDatabase( IServiceCollection services )
        {
            services.AddDatabase<ExampleContext>( Configuration.GetConnectionString( "ExampleConnection" ) );
        }
        public virtual void ConfigureHangFire(IServiceCollection services)
        {
            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("ExampleConnection")));
            services.AddHangfireServer();
            services.AddScoped<IHangFireService, HangFireService>();
        }
    }
}
