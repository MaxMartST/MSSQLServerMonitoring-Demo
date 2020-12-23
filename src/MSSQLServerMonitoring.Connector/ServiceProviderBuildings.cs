
using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Connector.Services;

namespace MSSQLServerMonitoring.Connector
{
    public static class ServiceProviderBuildings
    {
        // добавляем в сервис
        public static IServiceCollection AddMSSQLServerConnector(this IServiceCollection services, ConfigureMSSQLServerConnectorComponent configuration)
        {
            return services
                .AddSingleton(configuration)
                .AddScoped<IServiceMSSQLServer, ServiceMSSQLServer>();
        }
    }
}
