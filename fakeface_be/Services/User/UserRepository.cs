using fakeface_be.Models.User;
using MySql.Data.MySqlClient;

namespace fakeface_be.Services.User
{
    public class UserRepository : IUserRepository
    {
        public readonly IConfiguration _configuration;

        public UserRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        async Task<List<UserModel>> IUserRepository.GetAllUsers()
        {
            var result = new List<UserModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    string sqlQuery = "SELECT * FROM users;";

                    using (MySqlCommand command = new MySqlCommand(sqlQuery, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(new UserModel() {

                                    UserId = (int)reader["user_id"]

                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(1);
            }

            return result;
        }
    }
}
