namespace fakeface_be.Models.Post
{
    public class PostModel
    {
        public int PostId { get; set; }
        public int UsertId { get; set; }
        public string? Picture {  get; set; }

        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
