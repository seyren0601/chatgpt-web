using Microsoft.AspNetCore.Mvc;
using ChatGPTCaller.Models;

namespace ChatGPTCaller.Controllers
{
    [Route("register")]
    [ApiController]
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("/request")]
        public ActionResult<RegisterResponse> PostRegisterResult([FromBody]RegisterRequest request)
        {
            return new OkResult();
        }
    }
}
