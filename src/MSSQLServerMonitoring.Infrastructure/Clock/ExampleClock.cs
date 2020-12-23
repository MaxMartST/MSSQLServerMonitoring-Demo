using System;

namespace MSSQLServerMonitoring.Infrastructure.Clock
{
    public class ExampleClock : IClock
    {
        public DateTime Now()
        {
            return DateTime.UtcNow;
        }

        public DateTime Today()
        {
            return DateTime.UtcNow.Date;
        }
    }
}
