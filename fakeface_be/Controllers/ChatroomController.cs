using fakeface_be.Services.Chatroom;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/chatroom")]
    [ApiController]
    public class ChatroomController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IChatroomRepository _chatroomRepository;

        public ChatroomController(IConfiguration _configuration, IChatroomRepository _chatroomRepository)
        {
            this._configuration = _configuration;
            this._chatroomRepository = _chatroomRepository;
        }




    }
}
