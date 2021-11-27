using BusinessLogic.Workflow;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly Test _test;

        public HomeController(Test test)
        {
            _test = test;
        }

        [HttpGet("/") ]
        public IActionResult Index()
        {
            _test.Add("admin", "123456");
            return Ok();
        }
    }
}
