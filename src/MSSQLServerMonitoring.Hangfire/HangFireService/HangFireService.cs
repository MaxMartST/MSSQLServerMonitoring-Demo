using MSSQLServerMonitoring.Application.RawDataDownload;
using MSSQLServerMonitoring.Domain.HangFireModel;
using MSSQLServerMonitoring.Hangfire.EventBuffer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.HangFire.HangFire
{
    public class HangFireService : IHangFireService
    {
        private readonly SQLRawDataDownload _sQLRawDataDownload;
        private readonly IEventBufferRepository _eventBufferRepository;
        private readonly IHangFireCounterRepository _hangFireCounterRepository;
        public HangFireService(SQLRawDataDownload sQLRawDataDownload, IEventBufferRepository eventBufferRepository, IHangFireCounterRepository hangFireCounterRepository)
        {
            _sQLRawDataDownload = sQLRawDataDownload;
            _eventBufferRepository = eventBufferRepository;
            _hangFireCounterRepository = hangFireCounterRepository;
        }
        public async Task RunDemoTask()
        {
            HangFireCounter hangFireCounter = await _hangFireCounterRepository.GetHangFireCounter();

            if (hangFireCounter.Counter < hangFireCounter.Limit)
            {
                //await _sQLRawDataDownload.FilterOutNewSQLServerRequests();
                Console.WriteLine($"MSSQL server is being accessed. Counter: {hangFireCounter.Counter}, Limit: {hangFireCounter.Limit}.");
                hangFireCounter.Counter++;

                await _hangFireCounterRepository.UpdateHangFireCounter(hangFireCounter);
            }
            else
            {
                //await _eventBufferRepository.ClearEventSessionBuffer();
                Console.WriteLine($"Clearing the event session buffer. Counter: {hangFireCounter.Counter}, Limit: {hangFireCounter.Limit}.");
                hangFireCounter.Counter = 0;

                await _hangFireCounterRepository.UpdateHangFireCounter(hangFireCounter);
            }
        }
        public async Task RunSecondDemoTask()
        {
            Console.WriteLine("Analyzing requests for the last hour.");
        }
    }
}
