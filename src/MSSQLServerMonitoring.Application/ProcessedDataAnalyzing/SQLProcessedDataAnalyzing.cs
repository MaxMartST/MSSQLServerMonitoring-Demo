using MSSQLServerMonitoring.Domain.Model;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MSSQLServerMonitoring.Application.ProcessedDataAnalyzing
{
    class Coefficient
    {
        public Coefficient(double value1, double value2)
        {
            a = value1;
            b = value2;
        }

        public double a = 0, b = 0;
    }
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

            //DateTime yesterdayDate = new DateTime(2021, 1, 8, 17, 0, 0);//вчерашняя дата
            //List<Query> yesterdayListQueries = _repositoryWrapper.Query.GetOnCondition(q => q.TimeStamp >= yesterdayDate).Result;
            //var yesterdayGroupsQueries = yesterdayListQueries.GroupBy(q => q.SqlText);

            DateTime todayDate = new DateTime(2021, 1, 9, 17, 0, 0);//сегодняшняя дата, минус 1 час
            List<Query> todayListQueries = _repositoryWrapper.Query.GetOnCondition(q => q.TimeStamp >= todayDate).Result;//запросы сделаные 1 час назад
            var todayGroupsQueries = todayListQueries.GroupBy(q => q.SqlText);//сгруппирированные запросы по sql-text

            foreach (IGrouping<string, Query> group in todayGroupsQueries)
            {
                //ArithmeticMeanAnalysis(group);
                MethodLeastSquares(group, todayDate);
            }
            
            return todayListQueries;
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

        private void MethodLeastSquares(IGrouping<string, Query> queries, DateTime date)
        {
            DateTime yesterdayDate = date.AddDays(-1);
            List<Query> todayListQueries = _repositoryWrapper.Query.GetOnCondition(q => q.TimeStamp >= yesterdayDate && q.TimeStamp < date && q.SqlText == queries.Key).Result;
            long node = todayListQueries.Count;

            if (node != 0)
            {
                double[,] data;
                data = GetData(node, todayListQueries);
                Coefficient coefficient = GetApprox(data, node);
                
                Console.WriteLine("Коэффициенты:");
                Console.WriteLine($"a = {coefficient.a}");
                Console.WriteLine($"b = {coefficient.b}");
            }
        }
        private double[,] GetData(long node, List<Query> queries)
        {
            double[,] temp = new double[2, node];
            long i = 0;
            foreach (var query in queries)
            {
                temp[0, i] = query.LogicalReads;
                temp[1, i] = GetSeconds(query.TimeStamp);

                i++;
            }

            return temp;
        }

        private double GetSeconds(DateTime time)
        {
            return time.Minute * 60 + time.Second;
        }

        private Coefficient GetApprox(double[,] temp, long node)
        {
            double sumx = 0;
            double sumy = 0;
            double sumx2 = 0;
            double sumxy = 0;

            for (int i = 0; i < node; i++)
            {
                sumx += temp[0, i];
                sumy += temp[1, i];
                sumx2 += temp[0, i] * temp[0, i];
                sumxy += temp[0, i] * temp[1, i];
            }
            double a = (node * sumxy - (sumx * sumy)) / (node * sumx2 - sumx * sumx);
            double b = (sumy - a * sumx) / node;

            return new Coefficient(a, b);
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
