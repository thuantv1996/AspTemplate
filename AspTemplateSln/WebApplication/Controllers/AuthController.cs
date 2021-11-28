using BusinessLogic.Services.Abstract;
using Global.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/Register")]
        [AllowAnonymous]
        public IActionResult Register(UserDTO user)
        {
            var addUserResult = _userService.AddUser(user);
            return Ok(new ApiResult
            {
                Code = addUserResult.IsSuccess? 200 : 400,
                Data = null,
                Message = addUserResult.Message
            });
        }

        [HttpPost("/Login")]
        [AllowAnonymous]
        public IActionResult Login(UserDTO user)
        {
            var loginresult = _userService.Login(user);
            return Ok(new ApiResult
            {
                Code = loginresult.IsSuccess ? 200 : 400,
                Data = new { token = loginresult.Token },
                Message = loginresult.Message
            });
        }
    }
}
