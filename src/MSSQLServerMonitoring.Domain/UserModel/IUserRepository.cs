using System.Collections.Generic;
using System.Threading.Tasks;
using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.UserModel
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<User>> GetAll();
        Task<User> GetByUsername( string username );
        Task AddUser( User user );
        Task UpdateUser( User user );
    }
}
