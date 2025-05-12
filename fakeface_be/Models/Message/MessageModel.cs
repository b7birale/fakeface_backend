namespace fakeface_be.Models.Message
{
    public class MessageModel
    {
        public int MessageId { get; set; }
        public int SenderUserId { get; set; }
        public int RecieverUserId { get; set; }
        public int ChatroomId { get; set; }
        public string Content { get; set; }
        public DateTime MessageDatetime { get; set; }
    }
}
