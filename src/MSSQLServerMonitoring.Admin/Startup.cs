using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Infrastructure.Startup;

namespace MSSQLServerMonitoring.Admin
{
    public class Startup : IBaseStartup
    {
        public IConfiguration Configuration { get; }

        public Startup( IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices( IServiceCollection services )
        {
            AddServices( services );
            return services.BuildServiceProvider();
        }

        public virtual void AddServices( IServiceCollection services )
        {
            services.AddSpaStaticFiles( configuration => { configuration.RootPath = "ClientApp/dist"; } );
        }

        public virtual void Configure( IApplicationBuilder app )
        {
            app.UseDeveloperExceptionPageIfDev();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSpa( spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            } );
        }
    }
}
