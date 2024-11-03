using fakeface_be.Models.Post;

namespace fakeface_be.Services.Post
{
    public interface IPostRepository
    {
        Task<List<PostModel>> GetPostsByUserId(int user_id);

    }
}
