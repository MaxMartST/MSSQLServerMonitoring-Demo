using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Infrastructure.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MSSQLServerMonitoring.Application.ProcessedDataAnalyzing
{
    class Coefficient
    {
        public Coefficient(
            double logicalReadsA,
            double logicalReadsB,
            double WritesA,
            double WritesB,
            double DurationA,
            double DurationB)
        {
            _logicalReadsA = logicalReadsA;
            _logicalReadsB = logicalReadsB;
            _WritesA = WritesA;
            _WritesB = WritesB;
            _DurationA = DurationA;
            _DurationB = DurationB;
        }
        public double _logicalReadsA = 0, _logicalReadsB = 0;
        public double _WritesA = 0, _WritesB = 0;
        public double _DurationA = 0, _DurationB = 0;
    }
    class Deviation
    {
        public Deviation(double devLogicalReads, double devWrited, double devDuration)
        {
            deviationLogicalReads = devLogicalReads;
            deviationWrited = devWrited;
            deviationDuration = devDuration;
        }
        public double deviationLogicalReads = 0;
        public double deviationWrited = 0;
        public double deviationDuration = 0;
    }
    public class SQLProcessedDataAnalyzing : ISQLProcessedDataAnalyzing
    {
        IRepositoryWrapper _repositoryWrapper;
        public double percent = 10;
        private int numberParameters = 4;
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
                    GenerateAlert(query, $"Значение LogicalReads привышает {percent} процентов от нормы");
                }
                // Writes
                if (query.Writes > coeffWrites)
                {
                    GenerateAlert(query, $"Значение Writes привышает {percent} процентов от нормы");
                }
                // Duration
                if (query.Duration > coeffDuration)
                {
                    GenerateAlert(query, $"Значение Duration привышает {percent} процентов от нормы");
                }
            }
        }

        private void MethodLeastSquares(IGrouping<string, Query> queries, DateTime date)
        {
            DateTime yesterdayDate = date.AddDays(-1);
            List<Query> yesterdayListQueries = _repositoryWrapper.Query.GetOnCondition(q => q.TimeStamp >= yesterdayDate && q.TimeStamp < date && q.SqlText == queries.Key).Result;
            long node = yesterdayListQueries.Count;

            if (node != 0)
            {
                double[,] data;
                data = GetData(node, yesterdayListQueries);
                Coefficient coefficient = GetApprox(data, node);
                List<Query> todayListQueries = GetListQueries(queries);
                AnalysisByApproximationFunction(coefficient, todayListQueries, yesterdayListQueries);
            }
        }
        private void AnalysisByApproximationFunction(Coefficient coefficient, List<Query> todayListQueries, List<Query> yesterdayListQueries)
        {
            Deviation deviationYesterday = GetSumSquaresDeviations(coefficient, yesterdayListQueries);
            Deviation deviationToday = GetSumSquaresDeviations(coefficient, todayListQueries);

            if (CompareDeviations(deviationYesterday.deviationLogicalReads, deviationToday.deviationLogicalReads))
            {
                foreach (Query query in todayListQueries)
                {
                    GenerateAlert(query, $"Запрос привышает допустимое отклонение в {deviationYesterday.deviationLogicalReads} по значениею LogicalReads");
                }
            }

            if (CompareDeviations(deviationYesterday.deviationWrited, deviationToday.deviationWrited))
            {
                foreach (Query query in todayListQueries)
                {
                    GenerateAlert(query, $"Запрос привышает допустимое отклонение в {deviationYesterday.deviationWrited} по значениею Writed");
                }
            }

            if (CompareDeviations(deviationYesterday.deviationDuration, deviationToday.deviationDuration))
            {
                foreach (Query query in todayListQueries)
                {
                    GenerateAlert(query, $"Запрос привышает допустимое отклонение в {deviationYesterday.deviationDuration} по значениею Duration");
                }
            }
        }
        private bool CompareDeviations(double deviationYesterday, double deviationToday)
        {
            double interestRate = (percent + 100) / 100;

            return (deviationToday > (deviationYesterday * interestRate));
        }
        private double[,] GetData(long node, List<Query> queries)
        {
            double[,] temp = new double[numberParameters, node];
            long i = 0;
            foreach (var query in queries)
            {
                temp[0, i] = GetSeconds(query.TimeStamp);// x
                temp[1, i] = query.LogicalReads;// y
                temp[2, i] = query.Writes;// z
                temp[3, i] = (double)query.Duration;// t

                i++;
            }

            return temp;
        }

        private double GetSeconds(DateTime time)
        {
            return time.Minute * 60 + time.Second + time.Millisecond;
        }

        private Coefficient GetApprox(double[,] temp, long node)
        {
            //double sumx = 0;
            //double sumy = 0;
            //double sumx2 = 0;
            //double sumxy = 0;
            double sumTimeStamp = 0;// x
            double sumTimeStamp2 = 0;// x * x
            double sumLogicalReads = 0;// y
            double sumTimeStampOnLogicalReads = 0;// x * y
            double sumWrites = 0;// z
            double sumTimeStampOnWrites = 0;// x * z
            double sumDuration = 0;// t
            double sumTimeStampOnDuration = 0;// x * t

            for (int i = 0; i < node; i++)
            {
                //sumx += temp[0, i];
                //sumx2 += temp[0, i] * temp[0, i];
                //sumy += temp[1, i];
                //sumxy += temp[0, i] * temp[1, i];

                sumTimeStamp += temp[0, i];
                sumTimeStamp2 += temp[0, i] * temp[0, i];

                sumLogicalReads += temp[1, i];
                sumTimeStampOnLogicalReads += temp[0, i] * temp[1, i];

                sumWrites += temp[2, i];
                sumTimeStampOnWrites += temp[0, i] * temp[2, i];

                sumDuration += temp[3, i];
                sumTimeStampOnDuration += temp[0, i] * temp[3, i];
            }

            double logicalReadsA = (node * sumTimeStampOnLogicalReads - (sumTimeStamp * sumLogicalReads)) / (node * sumTimeStamp2 - sumTimeStamp * sumTimeStamp);
            double logicalReadsB = (sumLogicalReads - logicalReadsA * sumTimeStamp) / node;
            
            double WritesA = (node * sumTimeStampOnWrites - (sumTimeStamp * sumWrites)) / (node * sumTimeStamp2 - sumTimeStamp * sumTimeStamp);
            double WritesB = (sumWrites - WritesA * sumTimeStamp) / node;

            double DurationA = (node * sumTimeStampOnDuration - (sumTimeStamp * sumDuration)) / (node * sumTimeStamp2 - sumTimeStamp * sumTimeStamp);
            double DurationB = (sumWrites - DurationA * sumTimeStamp) / node;

            return new Coefficient(logicalReadsA, logicalReadsB, WritesA, WritesB, DurationA, DurationB);
        }

        private Deviation GetSumSquaresDeviations(Coefficient coefficient, List<Query> queries)
        {
            double sumDeviationLR = 0;
            double sumDeviationW = 0;
            double sumDeviationD = 0;

            foreach (var query in queries)
            {
                double x = GetSeconds(query.TimeStamp);

                // LogicalReads
                sumDeviationLR += GetSumDeviation(coefficient._logicalReadsA, coefficient._logicalReadsB, x, query.LogicalReads);

                // Writes
                sumDeviationW += GetSumDeviation(coefficient._WritesA, coefficient._WritesB, x, query.Writes);

                // Duration
                sumDeviationD += GetSumDeviation(coefficient._DurationA, coefficient._DurationB, x, (double)query.Duration);
            }

            return new Deviation(sumDeviationLR, sumDeviationW, sumDeviationD);
        }
        private double GetSumDeviation(double coefficientA, double coefficientB, double x, double y)
        {
            double temp = 0, value = 0;

            temp = coefficientA * x + coefficientB;
            value = y - temp;
            temp = value * value;

            return temp;
        }

        private void GenerateAlert(Query query, string message)
        {
            Alert alert = new Alert(
                query.SqlText,
                message, 
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
        private List<Query> GetListQueries(IGrouping<string, Query> queries)
        {
            List<Query> newListQueries = new List<Query>();

            foreach (Query query in queries)
            {
                newListQueries.Add(query);
            }

            return newListQueries;
        }
    }
}
