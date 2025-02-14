namespace fakeface_be.Models.Post
{
    public class PostFeedModel
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string? Picture { get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }

        public string? Title { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }

    }
}
