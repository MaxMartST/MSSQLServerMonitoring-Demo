using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MSSQLServerMonitoring.Infrastructure.Startup;

namespace MSSQLServerMonitoring.WabApp
{
    public class Startup : IBaseStartup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            AddServices(services);
            return services.BuildServiceProvider();
        }

        public virtual void AddServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public virtual void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPageIfDev();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();  // Добавляем маршрутизацию для Razor Pages
            });
        }
    }
}
