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
                            p.PostId = (int)reader["post_id"];
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
    }
}
