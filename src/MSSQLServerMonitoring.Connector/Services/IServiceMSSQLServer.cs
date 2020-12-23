using MSSQLServerMonitoring.Connector.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Connector.Services
{
    public interface IServiceMSSQLServer
    {
        List<EventMSSQLServer> GetNewQueryHistory();
    }
}
