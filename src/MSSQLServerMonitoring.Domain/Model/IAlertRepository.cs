using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Domain.Model
{
    public interface IAlertRepository : IRepository<Alert>, IRepositoryBase<Alert>
    {

    }
}
