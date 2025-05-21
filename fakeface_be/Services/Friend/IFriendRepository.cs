using fakeface_be.Models.Post;
using fakeface_be.Models.User;

namespace fakeface_be.Services.Friend
{
    public interface IFriendRepository
    {
        Task<List<int>> GetFriendsIdsByUserId(int user_id);
        Task<List<UserFriendModel>> GetFriendsByUserId(int user_id);

    }
}
