using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Hangfire.EventBuffer;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Infrastructure.Procedure
{
    public static class ProcedureBindings
    {
        public static IServiceCollection AddEventBuffer(this IServiceCollection services)
        {
            services.AddScoped<IEventBufferRepository, EventBufferRepository>();

            return services;
        }
    }
}
