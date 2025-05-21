
using fakeface_be.Models.Chatroom;
using fakeface_be.Models.Post;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Linq;

namespace fakeface_be.Services.Chatroom
{
    public class ChatroomRepository : IChatroomRepository
    {
        public readonly IConfiguration _configuration;

        public ChatroomRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public async Task<List<ChatroomModel>> GetChatroomsByUserId(int user_id)
        {
            var result = new List<ChatroomModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetChatroomsByUserId", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", user_id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChatroomModel c = new ChatroomModel();
                            c.ChatroomId = (int)reader["chatroom_id"];
                            c.Name = (string)reader["name"];
                            c.UserIdOne = (int)reader["user_id_one"];
                            c.UserIdTwo = (int)reader["user_id_two"];
                            c.ProfilePicture = reader.IsDBNull("profile_picture") ? "" : (string)reader["profile_picture"];
                            result.Add(c);
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


        public async Task<long> CreateChatroom(ChatroomModel chatroom)
        {
            long result = 0;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("CreateChatroom", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_name", chatroom.Name);
                    cmd.Parameters.AddWithValue("@p_user_id_one", chatroom.UserIdOne);
                    cmd.Parameters.AddWithValue("@p_user_id_two", chatroom.UserIdTwo);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result = Convert.ToInt64(reader["chatroom_id"]);
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

        public async Task<bool> DeleteChatroom(int chatroom_id)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("DeleteChatroom", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_room_id", chatroom_id); // param1 === adatbázisban lévő unpit név

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

        public async Task<ChatroomModel> GetChatroomByUserIds(ChatroomUserIdsModel user_ids)
        {
            var result = new ChatroomModel();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetChatroomByUserIds", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id_one", user_ids.UserIdOne);
                    cmd.Parameters.AddWithValue("@p_user_id_two", user_ids.UserIdTwo);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ChatroomModel c = new ChatroomModel();
                            c.ChatroomId = (int)reader["chatroom_id"];
                            c.Name = (string)reader["name"];
                            c.UserIdOne = (int)reader["user_id_one"];
                            c.UserIdTwo = (int)reader["user_id_two"];
                            result = c;
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
    }
}
