using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MSSQLServerMonitoring.Infrastructure.ServiceScope
{
    public class ServiceScopeWrapper : IServiceScopeWrapper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ServiceScopeWrapper( IServiceScopeFactory serviceScopeFactory )
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAction( Func<IServiceProvider, Task> action )
        {
            using ( IServiceScope serviceScope = _serviceScopeFactory.CreateScope() )
            {
                await action( serviceScope.ServiceProvider );
            }
        }
    }
}
