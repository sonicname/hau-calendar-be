using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using server.Models;
using server.Models.DTO;

namespace server.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private HauCalendarContext _calendarContext;
        public IConfiguration _configuration;

        public AuthController(IConfiguration config, HauCalendarContext calendarContext)
        {
            _calendarContext = calendarContext;
            _configuration = config;
        }

        [HttpPost("signin")]
        public IActionResult SignIn(UserDTO authParams)
        {
            if (String.IsNullOrEmpty(authParams.Username) || String.IsNullOrEmpty(authParams.Password))
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
            
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Username", user.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

        [HttpPost("signup")]
        public IActionResult SignUp(UserDTO authParams)
        {
            if (String.IsNullOrEmpty(authParams.Username) || String.IsNullOrEmpty(authParams.Password))
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

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("Username", authParams.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}
