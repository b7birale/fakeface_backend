using fakeface_be.Models.FriendRequest;
using fakeface_be.Models.Post;
using fakeface_be.Models.User;
using fakeface_be.Services.Friend;
using fakeface_be.Services.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/friend")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IFriendRepository _friendRepository;

        public FriendController(IConfiguration _configuration, IFriendRepository _friendRepository)
        {
            this._configuration = _configuration;
            this._friendRepository = _friendRepository;
        }

        [HttpGet("GetFriendsIds")]
        [Authorize]
        public async Task<ActionResult<List<int>>> GetFriendsIdsByUserId([FromQuery] string userId)
        {
            int.TryParse(userId, out int id);
            var result = await this._friendRepository.GetFriendsIdsByUserId(id);
            return Ok(result);

        }

        [HttpGet("GetFriends")]
        [Authorize]
        public async Task<ActionResult<List<UserFriendModel>>> GetFriendsByUserId([FromQuery] string userId)
        {
            int.TryParse(userId, out int id);
            var result = await this._friendRepository.GetFriendsByUserId(id);
            return Ok(result);

        }


        /*

        [HttpPost("add-friend")]
        public async Task<ActionResult<bool>> AddFriend([FromQuery] string user_id_one, [FromQuery] string user_id_two)
        {
            int.TryParse(user_id_one, out int userId1);
            int.TryParse(user_id_two, out int userId2);
            var result = await this._friendRepository.AddFriend(userId1, userId2);
            return Ok(result);

        }

        */

    }
}
