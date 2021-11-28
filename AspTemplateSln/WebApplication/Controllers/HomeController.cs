using BusinessLogic.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HomeController : ControllerBase
    {
        private readonly IUserService _userService;
        private IUserTokenService _userTokenService;

        public HomeController(IUserService userService, IUserTokenService userTokenService)
        {
            _userService = userService;
            _userTokenService = userTokenService;
        }

        [HttpGet("/GetUsers")]
        public IActionResult GetUsers()
        {
            return Ok("empty");
        }
    }
}
