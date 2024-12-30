using fakeface_be.Models.Post;
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

        [HttpGet("GetFriends")]
        [Authorize]
        public async Task<ActionResult<List<int>>> GetFriendsByUserId([FromQuery] string userId)
        {
            int.TryParse(userId, out int id);
            var result = await this._friendRepository.GetFriendsByUserId(id);
            return Ok(result);

        }
    }
}
