using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Domain.UserModel;

namespace MSSQLServerMonitoring.Infrastructure.Data.UserModel
{
    public class UserRepository : IUserRepository
    {
        private readonly ExampleContext _ctx;

        public UserRepository( ExampleContext ctx )
        {
            _ctx = ctx;
        }

        public Task<List<User>> GetAll()
        {
            return _ctx.User.ToListAsync();
        }

        public Task<User> GetByUsername( string username )
        {
            return _ctx.Set<User>()
                .Include( x => x.AssignedGroups )
                .ThenInclude( x => x.Group )
                .FirstOrDefaultAsync( x => x.Username == username );
        }

        public Task AddUser( User user )
        {
            _ctx.User.Add( user );
            return Task.CompletedTask;
        }

        public Task UpdateUser( User user )
        {
            _ctx.User.Update( user );
            return Task.CompletedTask;
        }
    }
}
