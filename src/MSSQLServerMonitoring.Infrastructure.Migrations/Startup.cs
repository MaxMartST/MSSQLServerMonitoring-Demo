using System;
using System.Linq;
using MSSQLServerMonitoring.Infrastructure.Clock;
using MSSQLServerMonitoring.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MSSQLServerMonitoring.Infrastructure.Migrations
{
    public class Startup : IStartup
    {
        private readonly IConfiguration _configuration;

        public Startup( IConfiguration configuration )
        {
            _configuration = configuration;
        }

        public IServiceProvider ConfigureServices( IServiceCollection services )
        {
            Console.WriteLine(string.Format(
                "Migration run as user: \"{0}\"",
                System.Security.Principal.WindowsIdentity.GetCurrent().Name ) );

            Console.WriteLine( string.Format(
                "Migration connection string: \"{0}\"",
                _configuration.GetConnectionString( "ExampleConnection" ) ) );

            services.AddDbContext<ExampleContext>( x =>
                    x.UseSqlServer( _configuration.GetConnectionString( "ExampleConnection" ) ) )
                .AddClock();

            return services.BuildServiceProvider();
        }

        public virtual void Configure( IApplicationBuilder app )
        {
            InitializeDatabase( app );
        }

        private void InitializeDatabase( IApplicationBuilder app )
        {
            using ( var scope = app.ApplicationServices.CreateScope() )
            {
                IClock clock = scope.ServiceProvider.GetService<IClock>();
                var contextFactory = new DesignTimeRepositoryContextFactory( clock );
                ExampleContext context = contextFactory.CreateDbContext( new string[] { } );
                context.Database.Migrate();

                string[] appliedMigrations = context.Database.GetAppliedMigrations().ToArray();
                Console.WriteLine( String.Join( "\n", appliedMigrations ) );
            }
        }
    }
}
