namespace fakeface_be.Models.User
{
    public class UserFriendModel
    {
        public int? UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
