using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.QueryModel
{
    public interface IQueryRepository : IRepository<Query>, IRepositoryBase<Query>
    {
    }
}
