using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace MSSQLServerMonitoring.Infrastructure.Startup
{
    public static class UseDeveloperExceptionPage
    {
        public static IApplicationBuilder UseDeveloperExceptionPageIfDev( this IApplicationBuilder app )
        {
            var env = app.ApplicationServices.GetService( typeof( IHostingEnvironment ) ) as IHostingEnvironment;

            if ( !env.IsEnvironment( "prod" ) )
            {
                return app.UseDeveloperExceptionPage();
            }

            return app;
        }
    }
}
