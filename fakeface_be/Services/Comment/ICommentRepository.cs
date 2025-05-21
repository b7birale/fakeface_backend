using fakeface_be.Models.Comment;

namespace fakeface_be.Services.Comment
{
    public interface ICommentRepository
    {
        Task<List<CommentFeedModel>> GetCommentsByPostId(int post_id);

        Task<bool> AddComment(int post_id, int user_id, string content);

    }
}
