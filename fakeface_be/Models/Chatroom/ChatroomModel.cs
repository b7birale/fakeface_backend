namespace fakeface_be.Models.Chatroom
{
    public class ChatroomModel
    {
        public int ChatroomId { get; set; }
        public string Name { get; set; }
        public int UserIdOne { get; set; }
        public int UserIdTwo { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
