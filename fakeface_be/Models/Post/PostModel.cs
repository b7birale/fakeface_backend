namespace fakeface_be.Models.Post
{
    public class PostModel
    {
        public int post_id { get; set; }
        public int user_id { get; set; }
        public string? Picture {  get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}
