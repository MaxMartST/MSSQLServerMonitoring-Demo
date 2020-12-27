using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Domain.QueryModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.AdminApi.Controllers
{
    public class StatisticsSQLServerController : Controller
    {
        private readonly SQLRawDataDownload _sQLRawDataDownload;
        public StatisticsSQLServerController(SQLRawDataDownload sQLRawDataDownload)
        {
            _sQLRawDataDownload = sQLRawDataDownload;
        }
        public List<Query> GetServerStatistics()
        {
            // Фильтруем запросы EventList, сохраняем только новые
            return _sQLRawDataDownload.FilterOutNewSQLServerRequests();
        }

    }
}
