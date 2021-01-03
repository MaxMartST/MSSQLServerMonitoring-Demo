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

        public async Task ClearEventSessionBuffer()
        {
            //_ctx.Database.ExecuteSqlCommandAsync("EXECUTE ClearEventSessionBuffer");
            using (var connection = _ctx.Database.GetDbConnection())
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "EXECUTE ClearEventSessionBuffer";
                    var result = await command.ExecuteNonQueryAsync();
                }
            }

        }
    }
}
