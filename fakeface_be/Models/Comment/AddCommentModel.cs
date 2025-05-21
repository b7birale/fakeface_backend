namespace fakeface_be.Models.Comment
{
    public class AddCommentModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }

    }
}
