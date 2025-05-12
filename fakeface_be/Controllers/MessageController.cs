using fakeface_be.Models.Message;
using fakeface_be.Models.User;
using fakeface_be.Services.Message;
using fakeface_be.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMessageRepository _messageRepository;

        public MessageController(IConfiguration _configuration, IMessageRepository _messageRepository)
        {
            this._configuration = _configuration;
            this._messageRepository = _messageRepository;
        }

        [HttpGet("get-messages")]
        public async Task<ActionResult<List<MessageModel>>> GetMessagesByChatroomId([FromQuery] string chatroom_id)
        {
            int.TryParse(chatroom_id, out int chatroomId);
            var result = await _messageRepository.GetMessagesByChatroomId(chatroomId);
            return Ok(result);

        }

        [HttpPost("send-message")]
        public async Task<ActionResult<bool>> SendMessage([FromBody] MessageModel message)
        {
            var result = await _messageRepository.SendMessage(message);
            return Ok(result);

        }

        [HttpPost("delete-message")]
        public async Task<ActionResult<bool>> DeleteMessage([FromQuery] string message_id)
        {
            int.TryParse(message_id, out int messageId);
            var result = await _messageRepository.DeleteMessage(messageId);
            return Ok(result);

        }

    }
}
