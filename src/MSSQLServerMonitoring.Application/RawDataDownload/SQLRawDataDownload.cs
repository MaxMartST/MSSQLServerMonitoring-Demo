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

        private List<Query> GetCompletedQuery(DateTime timeToAsk)
        {
            //List<EventMSSQLServer> ventMSSQLServers = //new List<EventMSSQLServer>();
            //ventMSSQLServers = _sQLServerServic.GetEventsFromSession();

            return _sQLServerServic.GetQueriesFromSQLServer(timeToAsk);
        }

        public List<Query> FilterOutNewSQLServerRequests()
        {
            //Фильтруем новые запросы на сервере SQL и сохраняем в БД
            var newQueries = new List<Query>();

            DateTime regDate = DateTime.Now;
            regDate = regDate.AddMinutes(-1);// время запросов выполненых минуту назад 

            var serverQueries = GetCompletedQuery(regDate);// получить запросы ссервера
            var dbQueries = _iQueryRepository.GetAll().Result;// получить запросы из БД

            if (dbQueries.Count == 0)
            {
                AddNewQueriesToBatabase(serverQueries);

                return serverQueries;
            }
            else
            {

                // Перебираем serverQueries, проверяем еть ли он в БД
                foreach (Query sQ in serverQueries)
                {
                    int index = dbQueries.FindIndex(dbQ => IdenticalRequests(sQ, dbQ));

                    if (index == -1)
                    {
                        newQueries.Add(sQ);
                    }
                }

                AddNewQueriesToBatabase(newQueries);

                return newQueries;
            }
        }

        private void AddNewQueriesToBatabase(List<Query> newQueries)
        {
            if (newQueries.Count != 0)
            {
                foreach (Query query in newQueries)
                {
                    _iQueryRepository.AddQuery(query);
                }
            }
        }

        private bool IdenticalRequests(Query sQ, Query dbQ)
        {
            if (sQ.AttachActivityId == dbQ.AttachActivityId)
            {
                return true;
            }
            //if (sQ.SqlText == dbQ.SqlText && sQ.DatabaseId == )

            return false;
        }
    }
}
