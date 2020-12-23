using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MSSQLServerMonitoring.Infrastructure.Startup
{
    public class Program<TStartup> where TStartup : IBaseStartup
    {
        public void Run( string[] args )
        {
            BuildWebHost( args ).Run();
        }

        private IWebHost BuildWebHost( string[] args )
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine( args )
                .Build();

            IWebHostBuilder builder = WebHost.CreateDefaultBuilder( args )
                .UseStartup( typeof( TStartup ) );
            string argUrls = config[ "urls" ];
            if ( argUrls != null )
            {
                builder.UseUrls( argUrls );
            }

            return builder.Build();
        }
    }
}
