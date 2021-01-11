using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Domain.HangFireModel;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Domain.UserModel;
using MSSQLServerMonitoring.Infrastructure.Data.HangFireModel;
using MSSQLServerMonitoring.Infrastructure.Data.QueryModel;
using MSSQLServerMonitoring.Infrastructure.Data.UserModel;

namespace MSSQLServerMonitoring.Infrastructure.Data
{
    public static class DataBindings
    {
        public static IServiceCollection AddRepositories( this IServiceCollection services )
        {
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<IHangFireCounterRepository, HangFireCounterRepository>();

            return services;
        }

        public static IServiceCollection AddDatabase<T>( this IServiceCollection services, string connectionString )
            where T : DbContext
        {
            return services.AddDbContext<T>( c =>
            {
                try
                {
                    c.UseLazyLoadingProxies().UseSqlServer( connectionString );
                }
                catch ( Exception )
                {
                    //TODO: logger
                }
            } );
        }
    }
}
