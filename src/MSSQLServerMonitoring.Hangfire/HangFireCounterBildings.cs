using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.HangFire.HangfireCounter;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.HangFire
{
    public static class HangFireCounterBildings
    {
        public static IServiceCollection AddHangFireTrafficController(this IServiceCollection services, HangFireCounter configuration)
        {
            services.AddSingleton(configuration);
            services.AddSingleton<IHangFireCounter>(configuration);

            return services;
        }
    }
}
