using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTCaller.Controllers
{
    [Route("login")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        LoginResponse Response;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("request")]
        public ActionResult<LoginResponse> PostLoginResult([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Response = _loginService.LoginUser(user.email, user.password);
                return Response;
            }
        }
    }
}
