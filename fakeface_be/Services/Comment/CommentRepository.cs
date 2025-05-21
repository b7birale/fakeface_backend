using fakeface_be.Models.Comment;
using fakeface_be.Models.Post;
using MySql.Data.MySqlClient;
using System.Data;
using System.Xml.Linq;

namespace fakeface_be.Services.Comment
{
    public class CommentRepository : ICommentRepository
    {
        public readonly IConfiguration _configuration;

        public CommentRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public async Task<List<CommentFeedModel>> GetCommentsByPostId(int post_id)
        {
            var result = new List<CommentFeedModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetCommentsByPostId", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_post_id", post_id); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CommentFeedModel c = new CommentFeedModel();
                            c.PostId = (int)reader["post_id"];
                            c.CommentId = (int)reader["comment_id"];
                            c.Content = (string)reader["content"];
                            c.FirstName = (string)reader["first_name"];
                            c.LastName = (string)reader["last_name"];
                            c.UserId = (int)reader["user_id"];
                            c.Date = (DateTime)reader["date"];
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

        public async Task<bool> AddComment(int post_id, int user_id, string content)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("AddComment", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_post_id", post_id);
                    cmd.Parameters.AddWithValue("@p_user_id", user_id);
                    cmd.Parameters.AddWithValue("@p_content", content);

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
