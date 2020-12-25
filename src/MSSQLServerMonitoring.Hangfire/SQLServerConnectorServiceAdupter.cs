using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Connector.Model;
using MSSQLServerMonitoring.Connector.Services;
using System;
using System.Collections.Generic;

namespace MSSQLServerMonitoring.Hangfire
{
    public class SQLServerConnectorServiceAdupter : ISQLServerService
    {
        IMSSQLServerService _serviceMSSQLServer;
        public SQLServerConnectorServiceAdupter(IMSSQLServerService serviceMSSQLServer)
        {
            _serviceMSSQLServer = serviceMSSQLServer;
        }

        public List<EventMSSQLServer> GetEventsFromSession()
        {
            List<EventMSSQLServer> ventMSSQLServers = new List<EventMSSQLServer>();

            ventMSSQLServers = _serviceMSSQLServer.GetNewQueryHistory();

            return ventMSSQLServers;
        }

        // конфигурируем serviceMSSQLServer
        // получаем сырые данные из MSSQL и мепим их в модель Domen из App
    }
}
