namespace fakeface_be.Models.FriendRequest
{
    public class SendFriendRequestModel
    {
        public int SenderUserId { get; set; }
        public int RecieverUserId { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
