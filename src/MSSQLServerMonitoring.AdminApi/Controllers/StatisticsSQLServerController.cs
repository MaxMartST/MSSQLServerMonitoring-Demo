using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Hangfire.EventBuffer;
using System.Collections.Generic;
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
        public List<Query> GetServerStatistics()
        {
            return _sQLRawDataDownload.FilterOutNewSQLServerRequests();
        }

        public async Task CleanBuffer()
        {
            await _eventBufferRepository.ClearEventSessionBuffer();
        }
    }
}
