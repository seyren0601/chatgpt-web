using Microsoft.AspNetCore.Mvc;
using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using System.Net;

namespace ChatGPTCaller.Controllers
{
    [Route("register")]
    [ApiController]
    public class RegisterController : Controller
    {
        private readonly RegisterService _registerService;
        RegisterResponse Response;
        public RegisterController(RegisterService registerService)
        {
            _registerService = registerService;
        }

        [HttpPost("request")]
        public ActionResult<RegisterResponse> PostRegisterResult([FromBody]User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Response = _registerService.RegisterUser(user);
                return Response;
            }
        }
    }
}
