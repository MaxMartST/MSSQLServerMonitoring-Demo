using System.Runtime.Serialization;

namespace MSSQLServerMonitoring.AdminApi.Dto
{
    [DataContract]
    public class UserDto
    {
        [DataMember( Name = "id" )]
        public int Id { get; set; }

        [DataMember( Name = "username" )]
        public string Username { get; set; }
    }
}
