using System.Threading.Tasks;
using MSSQLServerMonitoring.Infrastructure.Clock;
using MSSQLServerMonitoring.Infrastructure.Data;
using MSSQLServerMonitoring.IntegrationTests.TestKit;

namespace MSSQLServerMonitoring.IntegrationTests
{
    public class DatabaseSeeder : IDbSeeder
    {
        private readonly ExampleContext _context;
        private readonly IClock _clock;

        public DatabaseSeeder( ExampleContext context, IClock clock )
        {
            _context = context;
            _clock = clock;
        }

        public async Task Seed()
        {
            // TODO: Заполнение БД предустановленными данными, например списком валют
            await _context.SaveChangesAsync();
        }
    }
}
