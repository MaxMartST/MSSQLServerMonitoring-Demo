using System;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Infrastructure.ServiceScope
{
    public interface IServiceScopeWrapper
    {
        Task InvokeAction( Func<IServiceProvider, Task> action );
    }
}
