using fakeface_be.Models.Message;
using fakeface_be.Models.Post;
using MySql.Data.MySqlClient;
using System.Data;

namespace fakeface_be.Services.Message
{
    public class MessageRepository : IMessageRepository
    {
        public readonly IConfiguration _configuration;

        public MessageRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public async Task<List<MessageModel>> GetMessagesByChatroomId(int chatroom_id)
        {
            var result = new List<MessageModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetMessagesByChatroomId", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_chatroom_id", chatroom_id); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MessageModel m = new MessageModel();
                            m.MessageId = (int)reader["message_id"];
                            m.ChatroomId = (int)reader["chatroom_id"];
                            m.Content = (string)reader["content"];
                            m.RecieverUserId = (int)reader["reciever_user_id"];
                            m.SenderUserId = (int)reader["sender_user_id"];
                            m.MessageDatetime = (DateTime)reader["message_datetime"];
                            result.Add(m);
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

        public async Task<bool> SendMessage(MessageModel message)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();
                    
                    MySqlCommand cmd = new MySqlCommand("SendMessage", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_chatroom_id", message.ChatroomId);
                    cmd.Parameters.AddWithValue("@p_content", message.Content);
                    cmd.Parameters.AddWithValue("@p_sender_user_id", message.SenderUserId);
                    cmd.Parameters.AddWithValue("@p_reciever_user_id", message.RecieverUserId);
                    cmd.Parameters.AddWithValue("@p_message_datetime", message.MessageDatetime);

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

        public async Task<bool> DeleteMessage(int message_id)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("DeleteMessage", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_message_id", message_id); // param1 === adatbázisban lévő unpit név

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
    }
}
