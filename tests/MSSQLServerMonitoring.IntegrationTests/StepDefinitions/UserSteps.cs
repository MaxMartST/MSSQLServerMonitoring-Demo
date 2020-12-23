using System.Collections.Generic;
using System.Threading.Tasks;
using MSSQLServerMonitoring.AdminApi.Dto;
using MSSQLServerMonitoring.Domain.Toolkit.Domain.Abstractions;
using MSSQLServerMonitoring.Domain.UserModel;
using MSSQLServerMonitoring.IntegrationTests.ObjectMothers;
using MSSQLServerMonitoring.IntegrationTests.TestKit;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace MSSQLServerMonitoring.IntegrationTests.StepDefinitions
{
    public static class ClientSteps
    {
        public static async Task<User> GivenICreateUser(
            this ITestRunner testRunner,
            Users.MotherObject userMotherObject )
        {
            return await testRunner.GivenICreateUser( userMotherObject.Username );
        }

        public static async Task<User> GivenICreateUser(
            this ITestRunner testRunner,
            string username )
        {
            using ( IServiceScope scope = testRunner.Driver.Services().CreateScope() )
            {
                IUserRepository userRepository = scope.ServiceProvider.GetService<IUserRepository>();
                IUnitOfWork unitOfWork = scope.ServiceProvider.GetService<IUnitOfWork>();

                User user = new User( username );
                await userRepository.AddUser( user );
                await unitOfWork.SaveEntitiesAsync();

                return user;
            }
        }

        public static async Task ThenIHaveUsersFromApi(
            this ITestRunner testRunner,
            List<UserDto> expectedDto )
        {
            List<UserDto> actualDto = await testRunner.Driver.HttpClientGetAsync<List<UserDto>>( $"v1/users" );

            actualDto.Should().BeEquivalentTo( expectedDto );
        }
    }
}
