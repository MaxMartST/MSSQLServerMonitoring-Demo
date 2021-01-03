using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Hangfire.EventBuffer
{
    public interface IEventBufferRepository
    {
        Task ClearEventSessionBuffer();
    }
}
