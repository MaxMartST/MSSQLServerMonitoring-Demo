using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MSSQLServerMonitoring.Domain.UserModel;

namespace MSSQLServerMonitoring.Infrastructure.Data.UserModel
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ExampleContext _ctx;

        public GroupRepository( ExampleContext ctx )
        {
            _ctx = ctx;
        }
        
        public Task<List<Group>> GetAll()
        {
            return _ctx.Group.ToListAsync();
        }

        public Task AddGroup( Group group )
        {
            _ctx.Group.Add( group );
            return Task.CompletedTask;
        }
    }
}
