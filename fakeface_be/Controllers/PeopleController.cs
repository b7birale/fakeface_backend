using fakeface_be.Models.FriendRequest;
using fakeface_be.Models.User;
using fakeface_be.Services.Friend;
using fakeface_be.Services.People;
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
        private readonly IPeopleRepository peopleRepository;

        public PeopleController(IConfiguration configuration, IUserRepository userRepository, IFriendRepository friendRepository, IPeopleRepository peopleRepository)
        {
            this.configuration = configuration;
            this.userRepository = userRepository;
            this.friendRepository = friendRepository;
            this.peopleRepository = peopleRepository;
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
            var result = await peopleRepository.SendFriendRequest(dto.SenderUserId, dto.RecieverUserId);
            return Ok(result);

        }

        [HttpGet("get-requests")]
        public async Task<ActionResult<List<SendFriendRequestModel>>> GetFriendRequests([FromQuery] string user_id)
        {
            int.TryParse(user_id, out int userId);
            var result = await peopleRepository.GetFriendRequests(userId);
            return Ok(result);

        }

        [HttpPost("accept-friend-request")]
        public async Task<ActionResult<bool>> AcceptFriendRequest([FromBody] FriendRequestModel friend_request)
        {
            var result = await peopleRepository.AcceptFriendRequest(friend_request);
            return Ok(result);

        }

        [HttpPost("reject-friend-request")]
        public async Task<ActionResult<bool>> RejectFriendRequest([FromBody] FriendRequestModel friend_request)
        {
            var result = await peopleRepository.RejectFriendRequest(friend_request);
            return Ok(result);

        }

    }
}
