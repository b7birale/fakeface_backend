using fakeface_be.Models.Post;
using fakeface_be.Models.User;
using FakeFace_BE.DbContext;
using MySql.Data.MySqlClient;
using System.Data;

namespace fakeface_be.Services.Friend
{
    public class FriendRepository : IFriendRepository
    {
        public readonly IConfiguration _configuration;

        public FriendRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public async Task<List<int>> GetFriendsIdsByUserId(int user_id)
        {
            var result = new List<int>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetFriendsIdsByUserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", user_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add((int)reader["friend_id"]);
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

        public async Task<List<UserFriendModel>> GetFriendsByUserId(int user_id)
        {
            var result = new List<UserFriendModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetFriendsByUserId", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", user_id); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserFriendModel u = new UserFriendModel();
                            u.UserId = (int)reader["user_id"];
                            u.Firstname = (string)reader["first_name"];
                            u.Lastname = (string)reader["last_name"];
                            u.ProfilePicture = reader.IsDBNull("profile_picture") ? "" : (string)reader["profile_picture"];
                            result.Add(u);
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


        /*

        public async Task<bool> AddFriend(int user_id_one, int user_id_two)
        {
            var result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("CreateRelation", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_user_id_one", user_id_one);
                    cmd.Parameters.AddWithValue("@p_user_id_two", user_id_two);


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

        */


    }
}
