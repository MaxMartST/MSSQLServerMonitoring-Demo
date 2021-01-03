using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Hangfire.EventBuffer
{
    public class EventBufferRepository : IEventBufferRepository
    {
        private readonly ExampleContext _ctx;
        public EventBufferRepository(ExampleContext ctx)
        {
            _ctx = ctx;
        }

        public Task ClearEventSessionBuffer()
        {
            _ctx.Database.ExecuteSqlCommandAsync("EXECUTE ClearEventSessionBuffer");

            return Task.CompletedTask;
        }
    }
}
