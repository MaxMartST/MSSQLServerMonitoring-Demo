using System.Collections.Generic;
using System.Linq;
using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.UserModel
{
    public class User : Entity, IAggregateRoot
    {
         public User( string username )
        {
            Username = username;
        }
        
        public string Username { get; private set; }

        private readonly List<UserGroup> _assignedGroups = new List<UserGroup>();
        public virtual IReadOnlyCollection<UserGroup> AssignedGroups => _assignedGroups;
        
        public void AssignGroup( int groupId )
        {
            if ( _assignedGroups.Exists( x => x.GroupId == groupId ) )
            {
                return;
            }
            
            _assignedGroups.Add( new UserGroup( groupId ) );
        }

        public void DeclineGroup( int groupId )
        {
            UserGroup group = _assignedGroups.FirstOrDefault( x => x.GroupId == groupId ); 
            if ( group != null )
            {
                _assignedGroups.Remove( group );
            }
        }
        
        // For EF
        protected User()
        {
        }
    }
}
