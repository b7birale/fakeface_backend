using fakeface_be.Models.User;
using fakeface_be.Services.User;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace fakeface_be.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        [HttpGet(Name = "GetUserIds")]
        public async Task<ActionResult<List<UserModel>>> GetAllUsers()
        {
            var result = await userRepository.GetAllUsers();
            return Ok(result);
        }
    }

}
