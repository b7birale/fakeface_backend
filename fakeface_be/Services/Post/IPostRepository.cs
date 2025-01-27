using fakeface_be.Models.Post;

namespace fakeface_be.Services.Post
{
    public interface IPostRepository
    {
        Task<List<PostModel>> GetPostsByUserId(int user_id);

        Task<List<PostFeedModel>> GetPostsByUserIds(string userIds);

        Task<bool> CreatePost(PostModel post);

        Task<bool> DeletePost(int post_id);
    }
}
