using MSSQLServerMonitoring.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSSQLServerMonitoring.Infrastructure.Wrapper
{
    public interface IRepositoryWrapper
    {
        IQueryRepository Query { get; }
        IAlertRepository Alert { get; }
        void Save();
    }
}
