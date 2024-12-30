using fakeface_be.Models.LoginUser;
using fakeface_be.Models.Post;
using fakeface_be.Models.User;
using fakeface_be.Services.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fakeface_be.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPostRepository _postRepository;

        public PostController(IConfiguration _configuration, IPostRepository _postRepository)
        {
            this._configuration = _configuration;
            this._postRepository = _postRepository;
        }


        [HttpGet("GetPosts")]
        [Authorize]
        public async Task<ActionResult<List<PostModel>>> GetPostsByUserIds([FromQuery] string userIds)
        {
            var result = await this._postRepository.GetPostsByUserIds(userIds);
            return Ok(result);
            
        }


    }
}
