using MSSQLServerMonitoring.Infrastructure.Startup;

namespace MSSQLServerMonitoring.Admin
{
    public class Program
    {
        public static void Main( string[] args )
        {
            var program = new Program<Startup>();

            program.Run( args );
        }
    }
}
