using System;
using System.Threading;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> SaveEntitiesAsync( string traceId = null, CancellationToken cancellationToken = default(CancellationToken) );
    }
}