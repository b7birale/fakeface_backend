using fakeface_be.Models.User;
using fakeface_be.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;

        public UserController(IConfiguration configuration, IUserRepository userRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
        }

        
        [HttpGet("GetUserToProfile")]
        public async Task<ActionResult<UserModel>> GetUserToProfile([FromQuery] string user_id)
        {
            int.TryParse(user_id, out int userId);
            var result = await userRepository.GetUserToProfile(userId);
            return Ok(result);
            
        }

        [HttpPost("ModifyUserData")]
        public async Task<ActionResult<UpdateUserModel>> ModifyUserData([FromBody] UpdateUserModel user)
        {
            var result = await userRepository.ModifyUserData(user);
            return Ok(result);

        }

    }
}
