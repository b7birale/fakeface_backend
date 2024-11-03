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

        public Task<List<PostModel>> GetPostsByUserId(int user_id)
        {
            throw new NotImplementedException();
        }
    }
}
