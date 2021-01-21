using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Hangfire.EventBuffer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.AdminApi.Controllers
{
    public class StatisticsSQLServerController : Controller
    {
        private readonly SQLRawDataDownload _sQLRawDataDownload;
        private readonly IEventBufferRepository _eventBufferRepository;
        public StatisticsSQLServerController(SQLRawDataDownload sQLRawDataDownload, IEventBufferRepository eventBufferRepository)
        {
            _sQLRawDataDownload = sQLRawDataDownload;
            _eventBufferRepository = eventBufferRepository;

        }
        public async Task GetServerStatistics()
        {
            // Фильтруем запросы EventList, сохраняем только новые
            await _sQLRawDataDownload.FilterOutNewSQLServerRequests();
        }

        public async Task CleanBuffer()
        {
            //очищаем фильтр событий 
            await _eventBufferRepository.ClearEventSessionBuffer();
        }
    }
}
