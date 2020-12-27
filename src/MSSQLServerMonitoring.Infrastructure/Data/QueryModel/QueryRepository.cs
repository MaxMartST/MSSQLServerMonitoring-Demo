using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Domain.QueryModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Infrastructure.Data.QueryModel
{
    public class QueryRepository : IQueryRepository
    {
        private readonly ExampleContext _ctx;

        public QueryRepository(ExampleContext ctx)
        {
            _ctx = ctx;
        }
        public Task AddQuery(Query query)
        {
            _ctx.Query.Add(query);
            _ctx.SaveChanges();

            return Task.CompletedTask;
        }

        public Task<List<Query>> GetAll()
        {
            return _ctx.Query.ToListAsync();
        }

        public Task UpdateQuery(Query query)
        {
            _ctx.Update(query);

            return Task.CompletedTask;
        }
    }
}
