using fakeface_be.Services.Comment;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/comment")]
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



    }
}
