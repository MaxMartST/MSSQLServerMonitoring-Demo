using MSSQLServerMonitoring.Connector.Model;
using MSSQLServerMonitoring.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public interface ISQLServerService
    {
        //пока возращаем EventMSSQLServer, но нужна будет своя доменная модель
        List<Query> GetQueriesFromSQLServer(DateTime timeToAsk);//GetQueriesFromSQLServer
    }
}
