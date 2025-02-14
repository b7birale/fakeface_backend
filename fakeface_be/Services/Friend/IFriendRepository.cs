using fakeface_be.Models.Post;

namespace fakeface_be.Services.Friend
{
    public interface IFriendRepository
    {
        Task<List<int>> GetFriendsByUserId(int user_id);

    }
}
