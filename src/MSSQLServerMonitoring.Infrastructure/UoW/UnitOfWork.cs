using System.Threading;
using System.Threading.Tasks;
using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using MSSQLServerMonitoring.Infrastructure.Data;

namespace MSSQLServerMonitoring.Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ExampleContext _ctx;

        public UnitOfWork( ExampleContext ctx )
        {
            _ctx = ctx;
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }

        public async Task<bool> SaveEntitiesAsync( string traceId = null, CancellationToken cancellationToken = default( CancellationToken ) )
        {
            return await _ctx.SaveEntitiesAsync( traceId, cancellationToken );
        }
    }
}
