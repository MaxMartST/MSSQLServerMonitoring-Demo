using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Infrastructure.Clock;
using MSSQLServerMonitoring.Infrastructure.Data;
using MSSQLServerMonitoring.Infrastructure.ServiceScope;
using MSSQLServerMonitoring.Infrastructure.UoW;

namespace MSSQLServerMonitoring.Infrastructure.Startup
{
    public static class BaseBindings
    {
        public static IServiceCollection AddBaseServices( this IServiceCollection services )
        {
            return services
                .AddClock()
                .AddRepositories()
                .AddUnitOfWork()
                .AddTransient( typeof( IServiceScopeWrapper ), typeof( ServiceScopeWrapper ) );
        }
    }
}
