using fakeface_be.Models.LoginUser;
using fakeface_be.Models.User;
using fakeface_be.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fakeface_be.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IConfiguration _configuration, IUserRepository _userRepository)
        {
            this._configuration = _configuration;
            this._userRepository = _userRepository;
        }

        
        [HttpPost("GetUserById")]
        public async Task<ActionResult<UserModel>> GetUserById(int user_id)
        {
            var result = await _userRepository.GetUserById(user_id);
            return Ok(result);
        }
        
        
        [HttpPost("Login")]
        //[AllowAnonymous]
        public async Task<ActionResult<string>> AuthenticateAsync(LoginUserModel authenticationRequestBody)
        {
            
            var userFromDb = await _userRepository.Login(authenticationRequestBody.Email); //!!!
            var userHashedPassword = await _userRepository.HashPassword($"{authenticationRequestBody.Password}{userFromDb.Salt}");

            if (userFromDb.Password != userHashedPassword)
            {
                throw new Exception();
            }
            

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("Id", userFromDb.UserId.ToString()));
            claimsForToken.Add(new Claim("Email", authenticationRequestBody.Email)); //userFromDb.Email.ToString()
            claimsForToken.Add(new Claim("Firstname", userFromDb.Firstname));
            claimsForToken.Add(new Claim("Lastname", userFromDb.Lastname));
            claimsForToken.Add(new Claim("Birthdate", userFromDb.BirthDate.ToString()));
            //firstname, lastname, stb
            //amit belerakok a claimbe azt fogjuk tudni frontenden használni => PASSWORDOT TILOS BELERAKNI pont ezért

            var validationStart = DateTime.UtcNow;
            var validationEnd = DateTime.UtcNow.AddHours(1);

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                validationStart,
                validationEnd,
                signingCredentials
                );

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(new { token = tokenToReturn });
        }


        [HttpPost("SignUp")]
        //[AllowAnonymous]
        public async Task<ActionResult<bool>> SignUp(UserModel authenticationRequestBody)
        {

            var userFromDb = await _userRepository.SignUp(authenticationRequestBody);


            return Ok(userFromDb);
        }





    }

}




