using fakeface_be.Models.Message;
using fakeface_be.Models.Post;

namespace fakeface_be.Services.Message
{
    public interface IMessageRepository
    {
        Task<List<MessageModel>> GetMessagesByChatroomId(int chatroom_id);

        Task<bool> SendMessage(int chatroom_id, string content); // paraméterben nem vagyok biztos

        Task<bool> DeleteMessage (int message_id); //nem feltétlen szükséges
    }
}
