using Microsoft.AspNetCore.Identity;

namespace fakeface_be.Models.User
{
    public class UserModel : IdentityUser
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int UserId { get; set; }
        public DateOnly BirthDate { get; set; }

        public string? ProfilePicture { get; set; }
        public string? QRCode { get; set; }

        public string? Salt { get; set; }

    }
}
