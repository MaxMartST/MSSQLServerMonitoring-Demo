using MSSQLServerMonitoring.Connector.Model;
using MSSQLServerMonitoring.Domain.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.RawDataDownload
{
    public class SQLRawDataDownload : ISQLRawDataDownload
    {
        // логика приложения получить сырые данные по событиям и сохранить анамальные запросы
        ISQLServerService _sQLServerServic;
        IQueryRepository _iQueryRepository;
        List<Query> queries;
        public SQLRawDataDownload(ISQLServerService sQLServerServic, IQueryRepository iQueryRepository)
        {
            _sQLServerServic = sQLServerServic;
            _iQueryRepository = iQueryRepository;
        }

        private List<Query> GetCompletedQuery()
        {
            //List<EventMSSQLServer> ventMSSQLServers = //new List<EventMSSQLServer>();
            //ventMSSQLServers = _sQLServerServic.GetEventsFromSession();

            return _sQLServerServic.GetEventsFromSession();
        }

        public List<Query> FilterOutNewSQLServerRequests()
        {
            //Фильтруем новые запросы на сервере SQL и сохраняем в БД

            //List<Query> newQueries = new List<Query>();
            queries = _sQLServerServic.GetEventsFromSession();
            AddNewQueriesToBatabase(queries);

            return queries;
        }

        private void AddNewQueriesToBatabase(List<Query> newQueries)
        {
            foreach (Query query in queries)
            {
                _iQueryRepository.AddQuery(query);
            }
        }
    }
}
