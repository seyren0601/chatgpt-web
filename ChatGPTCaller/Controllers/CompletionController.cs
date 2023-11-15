using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatGPTCaller.Models;

namespace ChatGPTCaller.Controllers
{
    [Route("api")]
    [ApiController]
    public class CompletionController : ControllerBase
    {
        string response = "Hello World!";

        [HttpGet("completion")]
        public string CompletionAPI()
        {
            return response;
        }
    }
}
