using fakeface_be.Models.User;

namespace fakeface_be.Services.User
{
    public interface IUserRepository
    {
        Task<List<UserModel>> GetAllUsers();

    }
}
