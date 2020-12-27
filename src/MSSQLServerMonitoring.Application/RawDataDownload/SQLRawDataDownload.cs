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

            return _sQLServerServic.GetQueriesFromSQLServer();
        }

        public List<Query> FilterOutNewSQLServerRequests()
        {
            //Фильтруем новые запросы на сервере SQL и сохраняем в БД

            var newQueries = new List<Query>();
            var serverQueries = GetCompletedQuery();// получить запросы ссервера
            var requestsDb = _iQueryRepository.GetAll().Result;// получить запросы из БД

            // Перебираем serverQueries, проверяем еть ли он в БД
            foreach (Query sQuery in serverQueries)
            {
                int index = requestsDb.BinarySearch(sQuery);

                if (index < 0)
                {
                    newQueries.Add(sQuery);
                }
            }

            AddNewQueriesToBatabase(newQueries);

            return newQueries;
        }

        private void AddNewQueriesToBatabase(List<Query> newQueries)
        {
            if (newQueries != null)
            {
                foreach (Query query in newQueries)
                {
                    _iQueryRepository.AddQuery(query);
                }
            }
        }
    }
}
