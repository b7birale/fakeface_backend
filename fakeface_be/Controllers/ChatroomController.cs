using fakeface_be.Models.Chatroom;
using fakeface_be.Models.User;
using fakeface_be.Services.Chatroom;
using fakeface_be.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/chatrooms")]
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

        [HttpGet("get-chatrooms")]
        public async Task<ActionResult<List<ChatroomModel>>> GetChatroomsByUserId([FromQuery] string user_id)
        {
            int.TryParse(user_id, out int userId);
            var result = await _chatroomRepository.GetChatroomsByUserId(userId);
            return Ok(result);

        }

        [HttpPost("create-chatroom")]
        public async Task<ActionResult<int>> CreateChatroom([FromBody] ChatroomModel chatroom)
        {
            var dto = new ChatroomUserIdsModel();

            if (chatroom.UserIdOne > chatroom.UserIdTwo)
            {
                var p = chatroom.UserIdOne;
                chatroom.UserIdOne = chatroom.UserIdTwo;
                chatroom.UserIdTwo = p;
            }

            dto.UserIdOne = chatroom.UserIdOne;
            dto.UserIdTwo = chatroom.UserIdTwo;

            var chatroom_exist = await _chatroomRepository.GetChatroomByUserIds(dto);

            if(chatroom_exist != null && chatroom_exist.ChatroomId > 0)
            {
                return Ok(chatroom_exist.ChatroomId);
            }
            else
            {
                var result = await _chatroomRepository.CreateChatroom(chatroom);
                return Ok(result);
            }

        }


    }
}
