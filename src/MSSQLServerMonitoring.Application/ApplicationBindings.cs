using MSSQLServerMonitoring.Application.Calculator;
using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Application.ProcessedDataAnalyzing;

namespace MSSQLServerMonitoring.Application
{
    public static class ApplicationBindings
    {
        public static IServiceCollection AddApplication( this IServiceCollection services )
        {
            services.AddScoped<ICalculator, Calculator.Calculator>();
            services.AddScoped<SQLRawDataDownload>();
            services.AddScoped<SQLProcessedDataAnalyzing>();

            return services;
        }
    }
}
