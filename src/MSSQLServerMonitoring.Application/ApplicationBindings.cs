using MSSQLServerMonitoring.Application.Calculator;
using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Application.RawDataDownload;

namespace MSSQLServerMonitoring.Application
{
    public static class ApplicationBindings
    {
        public static IServiceCollection AddApplication( this IServiceCollection services )
        {
            services.AddScoped<ICalculator, Calculator.Calculator>();
            services.AddScoped<SQLRawDataDownload>();

            return services;
        }
    }
}
