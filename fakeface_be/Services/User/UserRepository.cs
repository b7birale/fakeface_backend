using fakeface_be.Models.Post;
using fakeface_be.Models.User;
using FakeFace_BE.DbContext;
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

        public async Task<UserModel> GetUserToProfile(int user_id)
        {
            var result = new UserModel();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetUserToProfile", connection); // tárolt eljárás neve
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
                            result.ProfilePicture = reader.IsDBNull("profile_picture") ? "" : (string)reader["profile_picture"];
                            var date = (DateTime)reader["birthdate"];
                            result.BirthDate = DateOnly.FromDateTime(date);

                            //birthdate, profile_picture
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
            }

            return result;
        }

        public async Task<bool> ModifyUserData(UpdateUserModel user)
        {
            var result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("ModifyUserData", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;

                    if(user.Password != "")
                    {
                        var salt = DateTime.Now.ToString();
                        var password = await HashPassword($"{user.Password}{salt}");
                        cmd.Parameters.AddWithValue("@p_password", password);
                        cmd.Parameters.AddWithValue("@p_salt", salt);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p_password", "");
                        cmd.Parameters.AddWithValue("@p_salt", "");
                    }                  
                    if (user.BirthDate == null)
                    {
                        cmd.Parameters.AddWithValue("@p_birthdate", "");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@p_birthdate", user.BirthDate.ToString());
                    }
                    cmd.Parameters.AddWithValue("@p_profile_picture", user.ProfilePicture);
                    cmd.Parameters.AddWithValue("@p_first_name", user.Firstname);
                    cmd.Parameters.AddWithValue("@p_last_name", user.Lastname);
                    cmd.Parameters.AddWithValue("@p_user_id", user.UserId);




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
            }

            return result;
        }

        public async Task<UserModel> Login(string email)
        {
            var result = new UserModel();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("Login", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Password = (string)reader["password"];
                            result.Salt = (string)reader["salt"];
                            result.Lastname = (string)reader["last_name"];
                            result.Firstname = (string)reader["first_name"];
                            result.UserId = (int)reader["user_id"];
                            var date = (DateTime)reader["birthdate"];
                            result.BirthDate = DateOnly.FromDateTime(date);
                            //result.BirthDate = (DateOnly)reader["birthdate"];
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
            }

            return result;
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

                    cmd.Parameters.AddWithValue("@birthdate", user.BirthDate.ToString());
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
                System.Diagnostics.Trace.TraceError(ex.Message + "\n" + ex.Message);
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


        public async Task<List<UserPersonModel>> GetAllUsers(int user_id)
        {
            var result = new List<UserPersonModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetAllUsers", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", user_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new UserPersonModel();
                            user.UserId = (int)reader["user_id"];
                            user.Firstname = (string)reader["first_name"];
                            user.Lastname = (string)reader["last_name"];
                            user.ProfilePicture = reader.IsDBNull(reader.GetOrdinal("profile_picture")) ? "" : reader.GetString(reader.GetOrdinal("profile_picture"));

                            result.Add(user);
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return result;
        }
    }
}
