using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MSSQLServerMonitoring.Domain
{
    public interface IRepositoryBase<T>
    {
        Task <List<T>> GetAll();
        Task <List<T>> GetOnCondition(Expression<Func<T, bool>> expression);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
