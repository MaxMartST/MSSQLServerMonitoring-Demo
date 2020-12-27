using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Domain.QueryModel
{
    public interface IQueryRepository : IRepository<Query>
    {
        Task<List<Query>> GetAll();
        Task AddQuery(Query query);
        Task UpdateQuery(Query query);
    }
}
