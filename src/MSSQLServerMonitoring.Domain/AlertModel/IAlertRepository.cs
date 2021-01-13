using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.AlertModel
{
    public interface IAlertRepository : IRepository<Alert>, IRepositoryBase<Alert>
    {

    }
}
