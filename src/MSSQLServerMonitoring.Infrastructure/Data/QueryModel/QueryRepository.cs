using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Domain.Model;
using MSSQLServerMonitoring.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Infrastructure.Data.QueryModel
{
    public class QueryRepository : RepositoryBase<Query>, IQueryRepository
    {
        public QueryRepository(ExampleContext ctx) : base(ctx)
        {
        }
    }
}
