using fakeface_be.Services.Message;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Controllers
{
    [Route("api/message")]
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





    }
}
