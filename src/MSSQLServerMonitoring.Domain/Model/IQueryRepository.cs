using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Domain.Model
{
    public interface IQueryRepository : IRepository<Query>, IRepositoryBase<Query>
    {
    }
}
