using fakeface_be.Models.Post;

namespace fakeface_be.Services.Friend
{
    public interface IFriendRepository
    {
        Task<List<int>> GetFriendsByUserId(int user_id);

        Task<bool> SendFriendRequest(int user_id_sender, int user_id_reciever);

    }
}
