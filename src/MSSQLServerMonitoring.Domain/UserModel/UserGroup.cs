using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.UserModel
{
    public class UserGroup : Entity
    {
        public UserGroup( int groupId )
        {
            GroupId = groupId;
        }
        
        public int UserId { get; private set; }
        
        public int GroupId { get; private set; }
        
        public virtual Group Group { get; private set; }
        
        // For EF
        protected UserGroup()
        {
        }
    }
}
