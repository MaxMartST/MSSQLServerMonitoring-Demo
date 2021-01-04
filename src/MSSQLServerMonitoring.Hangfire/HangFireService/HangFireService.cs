using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Hangfire.EventBuffer;
using MSSQLServerMonitoring.HangFire.HangfireCounter;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.HangFire.HangFire
{
    public class HangFireService : IHangFireService
    {
        private readonly IHangFireCounter _hangFireCounter;
        private readonly SQLRawDataDownload _sQLRawDataDownload;
        private readonly IEventBufferRepository _eventBufferRepository;
        public HangFireService(IHangFireCounter hangFireCounter, SQLRawDataDownload sQLRawDataDownload, IEventBufferRepository eventBufferRepository)
        {
            _hangFireCounter = hangFireCounter;
            _sQLRawDataDownload = sQLRawDataDownload;
            _eventBufferRepository = eventBufferRepository;
        }
        public async Task RunDemoTask()
        {
            if (_hangFireCounter.GetCounter() < _hangFireCounter.GetLimit())
            {
                await _sQLRawDataDownload.FilterOutNewSQLServerRequests();
                Console.WriteLine($"MSSQL server is being accessed. Counter: {_hangFireCounter.GetCounter()}, Limit: {_hangFireCounter.GetLimit()}");
                _hangFireCounter.AddCounter();
            }
            else
            {
                await _eventBufferRepository.ClearEventSessionBuffer();
                Console.WriteLine($"Clearing the event session buffer. Counter: {_hangFireCounter.GetCounter()}, Limit: {_hangFireCounter.GetLimit()}");
                _hangFireCounter.ResetCounter();
            }
        }
    }
}
