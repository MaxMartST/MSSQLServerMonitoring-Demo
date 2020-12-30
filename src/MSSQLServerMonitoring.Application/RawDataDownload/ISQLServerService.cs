using MSSQLServerMonitoring.Connector.Model;
using MSSQLServerMonitoring.Domain.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public interface ISQLServerService
    {
        //пока возращаем EventMSSQLServer, но нужна будет своя доменная модель
        List<Query> GetQueriesFromSQLServer(int id);//GetQueriesFromSQLServer
    }
}
