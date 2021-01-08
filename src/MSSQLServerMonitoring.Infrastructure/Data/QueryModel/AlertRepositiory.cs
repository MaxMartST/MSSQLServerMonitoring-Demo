using MSSQLServerMonitoring.Domain.Model;
using MSSQLServerMonitoring.Infrastructure.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Infrastructure.Data.QueryModel
{
    public class AlertRepositiory : RepositoryBase<Alert>, IAlertRepository
    {
        public AlertRepositiory(ExampleContext ctx) : base(ctx)
        {
        }
    }
}
