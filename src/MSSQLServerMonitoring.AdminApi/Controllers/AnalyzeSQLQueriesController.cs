using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.Application.ProcessedDataAnalyzing;
using MSSQLServerMonitoring.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.AdminApi.Controllers
{
    public class AnalyzeSQLQueriesController : Controller
    {
        private readonly SQLProcessedDataAnalyzing _sQLProcessedDataAnalyzing;
        public AnalyzeSQLQueriesController(SQLProcessedDataAnalyzing sQLProcessedDataAnalyzing)
        {
            _sQLProcessedDataAnalyzing = sQLProcessedDataAnalyzing;
        }
        public List<Query> GetAnalyze()
        {
            return _sQLProcessedDataAnalyzing.AnalyzeQueries();
        }
    }
}
