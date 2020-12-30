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

        private List<Query> GetCompletedQuery(int id)
        {
            //List<EventMSSQLServer> ventMSSQLServers = //new List<EventMSSQLServer>();
            //ventMSSQLServers = _sQLServerServic.GetEventsFromSession();

            return _sQLServerServic.GetQueriesFromSQLServer(id);
        }

        public List<Query> FilterOutNewSQLServerRequests()
        {
            //Фильтруем новые запросы на сервере SQL и сохраняем в БД

            var newQueries = new List<Query>();
            var serverQueries = GetCompletedQuery(11);// получить запросы ссервера
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
                    int index = dbQueries.FindIndex(dbQ => sQ.SqlText == dbQ.SqlText && sQ.TimeStamp == dbQ.TimeStamp && sQ.ClientHn == dbQ.ClientHn && sQ.DatabaseId == dbQ.DatabaseId && sQ.Duration == dbQ.Duration && sQ.CpuTime == dbQ.CpuTime && sQ.PhysicalReads == dbQ.PhysicalReads && sQ.LogicalReads == dbQ.LogicalReads && sQ.Writes == dbQ.Writes);

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

        private bool IdenticalQueties(Query sQ, Query dbQ)
        {
            if (sQ.SqlText == dbQ.SqlText && sQ.TimeStamp == dbQ.TimeStamp && sQ.ClientHn == dbQ.ClientHn && sQ.DatabaseId == dbQ.DatabaseId && sQ.Duration == dbQ.Duration && sQ.CpuTime == dbQ.CpuTime && sQ.PhysicalReads == dbQ.PhysicalReads && sQ.LogicalReads == dbQ.LogicalReads && sQ.Writes == dbQ.Writes)
            {
                return true;
            }
            //if (sQ.SqlText == dbQ.SqlText && sQ.DatabaseId == )

            return false;
        }
    }
}
