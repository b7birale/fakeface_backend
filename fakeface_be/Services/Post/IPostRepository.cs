using fakeface_be.Models.Post;

namespace fakeface_be.Services.Post
{
    public interface IPostRepository
    {
        Task<List<PostModel>> GetPostsByUserId(int user_id);

        Task<List<PostModel>> GetPostsByUserIds(string userIds);

        Task<bool> CreatePost(string picture, string content, int user_id);

        Task<bool> DeletePost(int post_id);
    }
}
