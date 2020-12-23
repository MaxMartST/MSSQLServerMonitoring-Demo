using System;

namespace MSSQLServerMonitoring.Infrastructure.Clock
{
    public interface IClock
    {
        DateTime Now();

        DateTime Today();
    }
}
