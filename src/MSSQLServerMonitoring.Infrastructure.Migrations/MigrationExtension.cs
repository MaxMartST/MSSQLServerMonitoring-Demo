using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace MSSQLServerMonitoring.Infrastructure.Migrations
{
    public static class MigrationExtension
    {
        public static IConfiguration GetConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath( Directory.GetCurrentDirectory() )
                .AddJsonFile( "appsettings.json" )
                .AddJsonFile( $"appsettings.{Environment.GetEnvironmentVariable( "ASPNETCORE_ENVIRONMENT" )}.json",
                    true )
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
