using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Application.RawDataDownload;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Hangfire
{
    public static class ServiceAdupterBuildings
    {
        public static IServiceCollection AddAdupter(this IServiceCollection services)
        {
            services.AddScoped<ISQLServerService, SQLServerConnectorServiceAdupter>();

            return services;
        }
    }
}
