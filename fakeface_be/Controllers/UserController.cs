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


        [HttpPost(Name = "SignUp")]
        public async Task<ActionResult<bool>> SignUp(UserModel user)
        {
            var date = user.BirthDate.ToString("yyyy-MM-dd");
            
            user.BirthDate = DateOnly.Parse(date);
            var result = await userRepository.SignUp(user);
            return Ok(result);
        }
    }
}
