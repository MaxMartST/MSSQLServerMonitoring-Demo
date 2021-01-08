using MSSQLServerMonitoring.Domain.Model;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.ProcessedDataAnalyzing
{
    public class SQLProcessedDataAnalyzing : ISQLProcessedDataAnalyzing
    {
        IRepositoryWrapper _repositoryWrapper;
        public SQLProcessedDataAnalyzing(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }
        public List<Query> AnalyzeQueries()
        {
            // Получить запросы из БД за час, сгруппировать эти запросы по sql-text
            //DateTime regDate = DateTime.Now;
            //regDate = regDate.AddHours(-1);// получить время час назад
            DateTime regDate = new DateTime(2021, 1, 8, 17, 0, 0);
            var dbQueries = _repositoryWrapper.Query.GetOnCondition(q => q.TimeStamp >= regDate).Result;

            return dbQueries;
        }
    }
}
