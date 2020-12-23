using Microsoft.Extensions.DependencyInjection;

namespace MSSQLServerMonitoring.Infrastructure.Clock
{
    public static class ClockBindings
    {
        public static IServiceCollection AddClock( this IServiceCollection services )
        {
            return services.AddSingleton( typeof( IClock ), typeof( ExampleClock ) );
        }
    }
}
