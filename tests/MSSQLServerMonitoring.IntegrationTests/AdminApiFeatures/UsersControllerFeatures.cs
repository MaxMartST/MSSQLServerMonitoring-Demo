using System.Collections.Generic;
using System.Threading.Tasks;
using MSSQLServerMonitoring.AdminApi.Dto;
using MSSQLServerMonitoring.Domain.UserModel;
using MSSQLServerMonitoring.IntegrationTests.ObjectMothers;
using MSSQLServerMonitoring.IntegrationTests.StepDefinitions;
using NUnit.Framework;

namespace MSSQLServerMonitoring.IntegrationTests.AdminApiFeatures
{
    public class UsersControllerFeatures : AdminApiFeature
    {
        [Test]
        public async Task GetUsers_Scenario()
        {
            // Given
            User user = await Runner.GivenICreateUser( Users.UserVasya );

            // When


            // Then
            await Runner.ThenIHaveUsersFromApi( new List<UserDto>
            {
                new UserDto
                {
                    Id = user.Id,
                    Username = user.Username
                }
            } );
        }
    }
}
