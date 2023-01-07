using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecretsSharing.Interface;
using SecretsSharing.Model;

namespace SecretsSharing.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        
        /// <summary>
        /// Get user manager from di
        /// </summary>
        /// <param name="userManager">instance userManager</param>
        public UserController(IUserManager userManager)
        {
            _userManager = userManager;
        }
        
        /// <summary>
        /// Register user and add him to database
        /// </summary>
        /// <param name="model">Authenticate model</param>
        /// <returns>Authenticate response with userId, email and token</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthModel model)
        {
            var response = await _userManager.Register(model);

            if (response == null)
                return BadRequest(new {message = "Didn't register!"});
            

            return Ok(response);
        }
        
        /// <summary>
        /// Sign in for register user
        /// </summary>
        /// <param name="model">Authenticate model</param>
        /// <returns>Authenticate response with userId, email and token</returns>
        [HttpPost("signin")]
        public IActionResult Authenticate(AuthModel model)
        {
            var response = _userManager.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            

            return Ok(response);
        }
    }
}