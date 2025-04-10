using fakeface_be.Models.User;

namespace fakeface_be.Services.User
{
    public interface IUserRepository
    {
        Task<List<UserPersonModel>> GetAllUsers(int user_id);

        Task<UserModel> GetUserById(int user_id);

        Task<UserModel> GetUserToProfile(int user_id);
        Task<bool> ModifyUserData(UpdateUserModel user);

        Task<bool> SignUp(UserModel user);

        Task<UserModel> Login(string email);

        Task<string> HashPassword(string password);

    }
}
