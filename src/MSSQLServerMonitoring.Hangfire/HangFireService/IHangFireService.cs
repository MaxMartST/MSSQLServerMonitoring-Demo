using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.HangFire.HangFire
{
    public interface IHangFireService
    {
        Task RunDemoTask();
    }
}
