using MSSQLServerMonitoring.Connector.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public class SQLRawDataDownload : ISQLRawDataDownload
    {
        // логика приложения получить сырые данные по событиям и сохранить анамальные запросы
        ISQLServerService _sQLServerServic;
        public SQLRawDataDownload(ISQLServerService sQLServerServic)
        {
            _sQLServerServic = sQLServerServic;
        }

        public List<EventMSSQLServer> GetCompletedQuery()
        {
            //List<EventMSSQLServer> ventMSSQLServers = //new List<EventMSSQLServer>();
            //ventMSSQLServers = _sQLServerServic.GetEventsFromSession();

            return _sQLServerServic.GetEventsFromSession();
        }
    }
}
