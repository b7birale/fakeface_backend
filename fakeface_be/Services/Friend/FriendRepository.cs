using fakeface_be.Models.Post;
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

        public async Task<List<int>> GetFriendsByUserId(int user_id)
        {
            var result = new List<int>();
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

        public async Task<bool> SendFriendRequest(int user_id_sender, int user_id_reciever)
        {
            var result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("CreateRequest", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_sender_user_id", user_id_sender);
                    cmd.Parameters.AddWithValue("@p_reciever_user_id", user_id_reciever);


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
    }
}
