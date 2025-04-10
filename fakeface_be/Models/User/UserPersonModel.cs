namespace fakeface_be.Models.User
{
    public class UserPersonModel
    {
        public int UserId { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
    }
}
