using MSSQLServerMonitoring.Domain.Model;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Query> listQuery = _repositoryWrapper.Query.GetOnCondition(q => q.TimeStamp >= regDate).Result;

            var queryGroups = listQuery.GroupBy(q => q.SqlText);
            foreach (IGrouping<string, Query> group in queryGroups)
            {
                ArithmeticMeanAnalysis(group);
            }
            
            return listQuery;
        }

        private void ArithmeticMeanAnalysis(IGrouping<string, Query> queries)
        {
            // Анализируем полученные запросы на среднее - Анализ среднего по
            // Duration
            // LogicalReads
            // Writes

            long average = 0;
            foreach (Query query in queries)
            {
                average += query.LogicalReads;
            }
            average /= queries.Count();

            double coefficient = average * 1.1;
            var alertList = new List<Alert>();
            foreach (Query query in queries)
            {
                if (query.LogicalReads > coefficient)
                {
                    //alertList.Add();
                    Console.WriteLine(query.AttachActivityId);
                }
            }
        }
    }
}
