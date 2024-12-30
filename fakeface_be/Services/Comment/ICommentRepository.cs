using fakeface_be.Models.Comment;

namespace fakeface_be.Services.Comment
{
    public interface ICommentRepository
    {
        Task<List<CommentModel>> GetCommentsByPostId(int post_id);

        Task<bool> AddComment(int post_id);

        Task<bool> DeleteComment(int comment_id);

    }
}
