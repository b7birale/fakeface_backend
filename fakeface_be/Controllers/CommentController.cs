using fakeface_be.Models.Comment;
using fakeface_be.Services.Comment;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ICommentRepository _commentRepository;

        public CommentController(IConfiguration _configuration, ICommentRepository _commentRepository)
        {
            this._configuration = _configuration;
            this._commentRepository = _commentRepository;
        }

        [HttpGet("GetComments")]
        [Authorize]
        public async Task<ActionResult<List<CommentModel>>> GetCommentsByPostId([FromQuery] string post_id)
        {
            int.TryParse(post_id, out int id);
            var result = await this._commentRepository.GetCommentsByPostId(id);
            return Ok(result);

        }

    }
}
