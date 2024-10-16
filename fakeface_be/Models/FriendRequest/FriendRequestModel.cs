namespace fakeface_be.Models.FriendRequest
{
    public class FriendRequestModel
    {
        public int RequestId { get; set; }
        public int SenderUserId { get; set; }
        public int RecieverUserId { get; set; }
        public bool Accepted { get; set; }
        public bool Rejected { get; set; }
    }
}
