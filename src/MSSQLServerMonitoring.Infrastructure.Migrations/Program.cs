using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace MSSQLServerMonitoring.Infrastructure.Migrations
{
    class Program
    {
        static void Main( string[] args )
        {
            new ConfigurationBuilder()
                .AddCommandLine( args )
                .Build();

            IWebHostBuilder builder = WebHost.CreateDefaultBuilder( args )
                .UseStartup( typeof( Startup ) );

            IWebHost webHost = builder.Build();
            webHost.Start();
            webHost.StopAsync( TimeSpan.Zero );
        }
    }
}
