using fakeface_be.Models.FriendRequest;
using fakeface_be.Models.User;
using fakeface_be.Services.Friend;
using fakeface_be.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/people")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IUserRepository userRepository;
        private readonly IFriendRepository friendRepository;

        public PeopleController(IConfiguration configuration, IUserRepository userRepository, IFriendRepository friendRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.friendRepository = friendRepository;
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserPersonModel>>> GetAllUsers([FromQuery] string user_id)
        {
            int.TryParse(user_id, out int userId);
            var result = await userRepository.GetAllUsers(userId);
            return Ok(result);

        }

        [HttpPost("friend-request")]
        public async Task<ActionResult<List<UserPersonModel>>> SendFriendRequest([FromBody] SendFriendRequestModel dto)
        {
            var result = await friendRepository.SendFriendRequest(dto.SenderUserId, dto.RecieverUserId);
            return Ok(result);

        }

    }
}
