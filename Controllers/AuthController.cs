using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.DTO.AuthDTOs;
using server.Models;

namespace server.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly HauCalendarContext _calendarContext;
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration config, HauCalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
            _configuration = config;
        }

        private JwtSecurityToken? ClaimToken(string username)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Username", username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);
            return token;
        }

        [HttpPost("signin")]
        public IActionResult SignIn(UserDTO authParams)
        {
            if (string.IsNullOrEmpty(authParams.Username) || string.IsNullOrEmpty(authParams.Password))
            {
                return BadRequest("Username or Password is required!");
            }

            var user = _calendarContext.Users.FirstOrDefault(user => user.Username == authParams.Username);

            if (user == null)
            {
                return NotFound();
            }

            if (user.Password != authParams.Password)
            {
                return BadRequest("Invalid credentials");
            }

            return Ok(new AuthResponseDTO()
            {
                userID = user.UserId,
                accessToken = new JwtSecurityTokenHandler().WriteToken(ClaimToken(user.Username)),
                username = user.Username
            });
        }

        [HttpPost("signup")]
        public IActionResult SignUp(UserDTO authParams)
        {
            if (string.IsNullOrEmpty(authParams.Username) || string.IsNullOrEmpty(authParams.Password))
            {
                return BadRequest("Username or Password is required!");
            }

            var user = _calendarContext.Users.FirstOrDefault(user => user.Username == authParams.Username);
            if (user != null)
            {
                return BadRequest("Username is already exists!");
            }

            _calendarContext.Users.Add(new User()
            {
                Username = authParams.Username,
                Password = authParams.Password
            });

            _calendarContext.SaveChanges();

            var userAfterSave = _calendarContext.Users.FirstOrDefault(user => user.Username == authParams.Username);

            return Ok(userAfterSave);
        }
    }
}
