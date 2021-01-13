using MSSQLServerMonitoring.Domain.AlertModel;
using MSSQLServerMonitoring.Infrastructure.Base;

namespace MSSQLServerMonitoring.Infrastructure.Data.AlertModel
{
    public class AlertRepository : RepositoryBase<Alert>, IAlertRepository
    {
        public AlertRepository(ExampleContext ctx) : base(ctx)
        {
        }
    }
}
