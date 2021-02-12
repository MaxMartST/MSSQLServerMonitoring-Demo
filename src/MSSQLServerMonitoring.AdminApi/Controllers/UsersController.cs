using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MSSQLServerMonitoring.AdminApi.Dto;
using MSSQLServerMonitoring.AdminApi.Mappers;
using MSSQLServerMonitoring.Domain.UserModel;

namespace MSSQLServerMonitoring.AdminApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/users")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<List<UserDto>> ListAll()
        {
            List<User> users = await _userRepository.GetAll();
            return users.Map();
        }

        [HttpPost]
        //public async Task AddUser(User user)
        //{
        //   await _userRepository.AddUser(user);
        //}

        public async Task AddUser(UserDto userDto)
        {
            var user = new User(userDto.Username);

            await _userRepository.AddUser(user);
        }
    }
}
