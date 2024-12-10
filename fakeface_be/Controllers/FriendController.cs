using fakeface_be.Services.Post;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    public class FriendController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IPostRepository _postRepository;

        public FriendController(IConfiguration _configuration, IPostRepository _postRepository)
        {
            this._configuration = _configuration;
            this._postRepository = _postRepository;
        }

        //getfriendsbyuser
    }
}
