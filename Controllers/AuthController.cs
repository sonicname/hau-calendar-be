using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Repository.Interface;

namespace server.Controllers
{
    [Route("/Api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository) => _userRepository = userRepository;

        [HttpGet("Users")]
        public IActionResult GetListUser()
        {
            return Ok(_userRepository.GetUsers());
        }

        [HttpGet("User")]
        public IActionResult GetUserByID(int id)
        {
            return Ok(_userRepository.GetUserByID(id));
        }

        [HttpPost("SignUp")]
        public IActionResult SignUp(UserAuthParams authParams)
        {
            if (String.IsNullOrEmpty(authParams.UserName) || String.IsNullOrEmpty(authParams.UserPassword))
            {
                return BadRequest("Username or Password is required!");
            }

            var user = _userRepository.UserIsExists(authParams.UserName);
            if (user)
            {
                return BadRequest("Username is already exists!");
            }
            
            _userRepository.AddUserToDb(authParams);

            return Ok("User created");
        }
    }
}
