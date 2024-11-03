using fakeface_be.Models.Post;
using fakeface_be.Models.User;
using Microsoft.AspNetCore.Identity;
using MySql.Data.MySqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace fakeface_be.Services.User
{
    public class UserRepository : IUserRepository
    {
        public readonly IConfiguration _configuration;

        public UserRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public async Task<UserModel> GetUserById(int user_id)
        {
            var result = new UserModel();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetUserById", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user_id", user_id); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.UserId = (int)reader["user_id"];
                            result.Email = (string)reader["email"];
                            result.Lastname = (string)reader["last_name"];
                            result.Firstname = (string)reader["first_name"];
                            // oszlopok
                            //Console.WriteLine($"{reader["user_id"]}, {reader["email"]}, {reader["first_name"]}");
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

        public async Task<UserModel> Login(string email)
        {
            throw new NotImplementedException();
            // többi alapján megírbi a tárolt eljárás hívást
        }

        public async Task<bool> SignUp(UserModel user)
        {
            var result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("SignUp", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", user.Email); // param1 === adatbázisban lévő unpit név

                    var salt = DateTime.Now.ToString();
                    var password = await HashPassword($"{user.Password}{salt}");
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@salt", salt);

                    cmd.Parameters.AddWithValue("@birthdate", user.BirthDate);
                    cmd.Parameters.AddWithValue("@profile_picture", user.ProfilePicture);
                    cmd.Parameters.AddWithValue("@first_name", user.Firstname);
                    cmd.Parameters.AddWithValue("@last_name", user.Lastname);
                    cmd.Parameters.AddWithValue("@qr_code", user.QRCode);
                    


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = true;
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


        public async Task<string> HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();

            var passwordBytes = Encoding.Default.GetBytes(password);

            var hashedPassword = hash.ComputeHash(passwordBytes);

            return Convert.ToHexString(hashedPassword);
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
