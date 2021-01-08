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
        public double percent = 10;
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
            long AverageLogicalReads = 0, AverageWrites = 0, count = queries.Count();
            decimal AverageDuration = 0;

            foreach (Query query in queries)
            {
                AverageLogicalReads += query.LogicalReads;
                AverageWrites += query.Writes;
                AverageDuration += query.Duration;
            }

            AverageLogicalReads /= count;
            AverageWrites /= count;
            AverageDuration /= count;

            double coefficient = (percent + 100) / 100;
            double coeffLogicalReads = AverageLogicalReads * coefficient;
            double coeffWrites = AverageWrites * coefficient;
            decimal coeffDuration = AverageDuration * (decimal)coefficient;

            foreach (Query query in queries)
            {
                // LogicalReads
                if (query.LogicalReads > coeffLogicalReads)
                {
                    GenerateAlert(query, "LogicalReads");
                }
                // Writes
                if (query.Writes > coeffWrites)
                {
                    GenerateAlert(query, "Writes");
                }
                // Duration
                if (query.Duration > coeffDuration)
                {
                    GenerateAlert(query, "Duration");
                }
            }
        }

        private void GenerateAlert(Query query, string message)
        {
            Alert alert = new Alert(
                query.SqlText, 
                $"Значение {message} привышает {percent} процентов от нормы", 
                query.AttachActivityId
            );
            alert.RegDate = DateTime.Now;

            Console.WriteLine(alert.AttachActivityId);
            Console.WriteLine(alert.TimeStamp);
            Console.WriteLine(alert.SqlText);
            Console.WriteLine(alert.Message);
            Console.WriteLine();
            //_repositoryWrapper.Alert.Add(alert);
        }
    }
}
