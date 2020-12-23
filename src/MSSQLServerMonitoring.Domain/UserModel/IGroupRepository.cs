using System.Collections.Generic;
using System.Threading.Tasks;
using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.UserModel
{
    public interface IGroupRepository : IRepository<Group>
    {
        Task<List<Group>> GetAll();
        
        Task AddGroup( Group group );
    }
}
