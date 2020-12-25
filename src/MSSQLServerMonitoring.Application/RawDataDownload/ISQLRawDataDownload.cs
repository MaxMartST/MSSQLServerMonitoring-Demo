using MSSQLServerMonitoring.Connector.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public interface ISQLRawDataDownload
    {
        List<EventMSSQLServer> GetCompletedQuery();
    }
}
