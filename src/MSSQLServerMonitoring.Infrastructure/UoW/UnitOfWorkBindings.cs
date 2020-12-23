using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace MSSQLServerMonitoring.Infrastructure.UoW
{
    public static class UnitOfWorkBindings
    {
        public static IServiceCollection AddUnitOfWork( this IServiceCollection services )
        {
            return services.AddScoped( typeof( IUnitOfWork ), typeof( UnitOfWork ) );
        }
    }
}
