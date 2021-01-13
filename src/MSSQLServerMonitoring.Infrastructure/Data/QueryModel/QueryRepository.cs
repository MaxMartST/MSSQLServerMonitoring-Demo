using MSSQLServerMonitoring.Domain.QueryModel;
using MSSQLServerMonitoring.Infrastructure.Base;

namespace MSSQLServerMonitoring.Infrastructure.Data.QueryModel
{
    public class QueryRepository : RepositoryBase<Query>, IQueryRepository
    {
        public QueryRepository(ExampleContext ctx) : base(ctx)
        {
        }
    }
}
