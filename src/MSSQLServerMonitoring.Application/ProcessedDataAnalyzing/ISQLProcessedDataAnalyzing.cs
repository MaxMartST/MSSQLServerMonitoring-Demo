using MSSQLServerMonitoring.Domain.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Application.ProcessedDataAnalyzing
{
    interface ISQLProcessedDataAnalyzing
    {
        List<Query> AnalyzeQueries();
    }
}
