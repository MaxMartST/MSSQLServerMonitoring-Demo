using MSSQLServerMonitoring.Connector.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Connector.Services
{
    public interface IMSSQLServerService
    {
        List<EventMSSQLServer> GetNewQueryHistory();
    }
}
