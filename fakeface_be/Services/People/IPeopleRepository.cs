using fakeface_be.Models.FriendRequest;
using Microsoft.AspNetCore.Mvc;

namespace fakeface_be.Services.People
{
    public interface IPeopleRepository
    {
        Task<bool> AcceptFriendRequest(FriendRequestModel friend_request);

        Task<bool> RejectFriendRequest(FriendRequestModel friend_request);

        Task<List<SendFriendRequestModel>> GetFriendRequests(int user_id);

        Task<bool> SendFriendRequest(int user_id_sender, int user_id_reciever);
    }
}
