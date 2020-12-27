using AutoMapper;
using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Connector.Model;
using MSSQLServerMonitoring.Connector.Services;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Hangfire.Mappers;
using System.Collections.Generic;

namespace MSSQLServerMonitoring.Hangfire
{
    public class SQLServerConnectorServiceAdupter : Profile, ISQLServerService
    {
        IMSSQLServerService _serviceMSSQLServer;
        public SQLServerConnectorServiceAdupter(IMSSQLServerService serviceMSSQLServer)
        {
            _serviceMSSQLServer = serviceMSSQLServer;
        }

        public List<Query> GetQueriesFromSQLServer()
        {
            List<Query> queries = new List<Query>();
            List<EventMSSQLServer> ventMSSQLServers = _serviceMSSQLServer.GetNnewHistoryQueriesSQLServer();//get a new history of requests


            return ventMSSQLServers.Map();
        }

        // конфигурируем serviceMSSQLServer
        // получаем сырые данные из MSSQL и мепим их в модель Domen из App
    }
}
