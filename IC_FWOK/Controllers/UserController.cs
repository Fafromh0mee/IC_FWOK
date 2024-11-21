using IC_FWOK.Models;
using IC_FWOK.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IC_FWOK.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            var result = await _userService.RegisterUser(user);
            if (!result)
            {
                return BadRequest("User already exists.");
            }
            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = await _userService.LoginUser(username, password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }
            return Ok("Login successful.");
        }
    }
}

