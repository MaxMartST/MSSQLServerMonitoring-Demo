using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;

namespace MSSQLServerMonitoring.Domain.UserModel
{
    public class Group : Entity, IAggregateRoot
    {
        public Group( string name )
        {
            this.Name = name;
        }
        
        public string Name { get; private set; }
        
        // For EF
        protected Group()
        {
        }
    }
}
