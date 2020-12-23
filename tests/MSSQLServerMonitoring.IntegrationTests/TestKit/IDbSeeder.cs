using System.Threading.Tasks;

namespace MSSQLServerMonitoring.IntegrationTests.TestKit
{
    public interface IDbSeeder
    {
        Task Seed();
    }
}
