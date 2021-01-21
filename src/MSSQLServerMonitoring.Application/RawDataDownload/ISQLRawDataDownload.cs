using MSSQLServerMonitoring.Connector.Model;
using MSSQLServerMonitoring.Domain.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public interface ISQLRawDataDownload
    {
        Task FilterOutNewSQLServerRequests();
    }
}
