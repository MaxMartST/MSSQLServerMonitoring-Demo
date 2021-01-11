using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Domain.HangFireModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Infrastructure.Data.HangFireModel
{
    public class HangFireCounterRepository : IHangFireCounterRepository
    {
        private readonly ExampleContext _ctx;

        public HangFireCounterRepository(ExampleContext ctx)
        {
            _ctx = ctx;
        }

        public Task<HangFireCounter> GetHangFireCounter()
        {
            return _ctx.HangFireCounter.FirstAsync();
        }

        public Task UpdateHangFireCounter(HangFireCounter hangFireCounter)
        {
            _ctx.Update(hangFireCounter);
            _ctx.SaveChanges();

            return Task.CompletedTask;
        }
    }
}
