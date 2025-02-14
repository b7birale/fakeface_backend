using fakeface_be.Models.Chatroom;

namespace fakeface_be.Services.Chatroom
{
    public interface IChatroomRepository
    {
        Task<List<ChatroomModel>> GetChatroomsByUserId(int user_id);

        Task<bool> CreateChatroom(string name);

        Task<bool> DeleteChatroom(int chatroom_id);

        //Task<bool> ShowChatroom(int chatroom_id);

    }
}
