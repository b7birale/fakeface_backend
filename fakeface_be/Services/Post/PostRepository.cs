using fakeface_be.Models.Post;
using fakeface_be.Models.User;
using MySql.Data.MySqlClient;
using System.Data;

namespace fakeface_be.Services.Post
{
    public class PostRepository : IPostRepository
    {
        public readonly IConfiguration _configuration;

        public PostRepository(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }

        public async Task<List<PostModel>> GetPostsByUserId(int user_id)
        {
            var result = new List<PostModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetPostsByUserIds", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_id", 6); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PostModel p = new PostModel();
                            p.post_id = (int)reader["post_id"];
                            p.Content = (string)reader["content"];
                            result.Add(p);
                            //Console.WriteLine($"{reader["user_id"]}, {reader["email"]}, {reader["first_name"]}");
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

        public async Task<List<PostFeedModel>> GetPostsByUserIds(string userIds)
        {
            var result = new List<PostFeedModel>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("GetPostsByUserIds", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_user_ids", userIds); // param1 === adatbázisban lévő unpit név

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PostFeedModel p = new PostFeedModel();
                            p.PostId = (int)reader["post_id"];
                            p.Content = (string)reader["content"];
                            p.Title = (string)reader["title"];
                            p.Firstname = (string)reader["first_name"];
                            p.Lastname = (string)reader["last_name"];
                            p.Date = (DateTime)reader["date"];
                            p.Picture = (string)reader["picture"];
                            result.Add(p);
                            //Console.WriteLine($"{reader["user_id"]}, {reader["email"]}, {reader["first_name"]}");
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

        public async Task<bool> CreatePost(PostModel post)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("CreatePost", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_picture", post.Picture); // param1 === adatbázisban lévő unpit név
                    cmd.Parameters.AddWithValue("@p_content", post.Content);
                    cmd.Parameters.AddWithValue("@p_user_id", post.user_id);
                    cmd.Parameters.AddWithValue("@p_title", post.Title);

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

        public async Task<bool> DeletePost(int post_id)
        {
            bool result = false;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(this._configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("DeletePost", connection); // tárolt eljárás neve
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@p_post_id", post_id); // param1 === adatbázisban lévő unpit név

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
