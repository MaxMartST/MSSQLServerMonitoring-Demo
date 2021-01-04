using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.HangFire.HangfireCounter
{
    public interface IHangFireCounter
    {
        int GetLimit();
        int GetCounter();
        void AddCounter();
        void ResetCounter();
    }
}
