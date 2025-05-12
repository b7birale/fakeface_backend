using fakeface_be.Models.FriendRequest;
using fakeface_be.Models.Post;
using MySql.Data.MySqlClient;
using System.Data;

namespace fakeface_be.Services.People
{
    public class PeopleRepository : IPeopleRepository
    {
        public readonly IConfiguration _configuration;

        public PeopleRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
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

        
        public async Task<bool> AcceptFriendRequest(FriendRequestModel friend_request)
        {
            var result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("AcceptRequest", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_user_id_sender", friend_request.SenderUserId);
                    cmd.Parameters.AddWithValue("@p_user_id_reciever", friend_request.RecieverUserId);


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


        public async Task<bool> RejectFriendRequest(FriendRequestModel friend_request)
        {
            var result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("RejectRequest", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@p_user_id_sender", friend_request.SenderUserId);
                    cmd.Parameters.AddWithValue("@p_user_id_reciever", friend_request.RecieverUserId);


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
        

        public async Task<List<SendFriendRequestModel>> GetFriendRequests(int user_id)
        {
            var result = new List<SendFriendRequestModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetIncomingFriendRequests", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", user_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            SendFriendRequestModel f = new SendFriendRequestModel();
                            f.SenderUserId = (int)reader["sender_user_id"];
                            f.RecieverUserId = (int)reader["reciever_user_id"];
                            f.Firstname = (string)reader["first_name"];
                            f.Lastname = (string)reader["last_name"];
                            f.ProfilePicture = reader.IsDBNull("profile_picture") ? "" : (string)reader["profile_picture"];
                            result.Add(f);
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
