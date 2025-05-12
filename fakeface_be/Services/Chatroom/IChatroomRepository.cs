using fakeface_be.Models.Chatroom;

namespace fakeface_be.Services.Chatroom
{
    public interface IChatroomRepository
    {
        Task<List<ChatroomModel>> GetChatroomsByUserId(int user_id);
        Task<ChatroomModel> GetChatroomByUserIds(ChatroomUserIdsModel user_ids);

        Task<long> CreateChatroom(ChatroomModel chatroom);

        Task<bool> DeleteChatroom(int chatroom_id);

        //Task<bool> ShowChatroom(int chatroom_id);

    }
}
