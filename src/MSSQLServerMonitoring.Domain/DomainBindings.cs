using Microsoft.Extensions.DependencyInjection;

namespace MSSQLServerMonitoring.Domain
{
    public static class DomainBindings
    {
        public static IServiceCollection AddDomain( this IServiceCollection services )
        {
            return services;
        }
    }
}
