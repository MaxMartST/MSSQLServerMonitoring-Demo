using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MSSQLServerMonitoring.Infrastructure.Clock;
using MSSQLServerMonitoring.Infrastructure.Data;
using MSSQLServerMonitoring.IntegrationTests.TestKit;

namespace MSSQLServerMonitoring.IntegrationTests
{
    public class AdminApiStartup : AdminApi.Startup
    {
        public AdminApiStartup( IConfiguration configuration, IHostingEnvironment env ) : base( configuration, env )
        {
        }

        public override void ConfigureDatabase( IServiceCollection services )
        {
            string dbName = Guid.NewGuid().ToString();
            services.AddDbContext<ExampleContext>(
                options => options
                    .UseInMemoryDatabase( dbName )
                    .ConfigureWarnings( x => x.Throw( RelationalEventId.QueryClientEvaluationWarning ) ) );
        }

        public override void AddServices( IServiceCollection services )
        {
            base.AddServices( services );

            services.AddSingleton<IDbSeeder, DatabaseSeeder>();
            services.RemoveAll<IClock>();
            services.AddSingleton<IClock, TestClock>();
        }
    }
}
