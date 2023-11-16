using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChatGPTCaller.Models;
using System.Security.Cryptography.X509Certificates;
using ChatGPTCaller.Services;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System;
using System.Net;

namespace ChatGPTCaller.Controllers
{
    [Route("api")]
    [ApiController]
    public class CompletionController : ControllerBase
    {
        ChatGPT_API_Response.APIResponse Response { get; set; }
        HttpStatusCode StatusCode { get; set; }
        private readonly ChatGPTService _chatGPTService;
        public CompletionController(ChatGPTService chatGPTService)
        {
            _chatGPTService = chatGPTService;
        }

        [HttpGet("completion")]
        public ActionResult<string> Get()
        {
            
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult(400);
            }
            else
            {
                return "Hello World!";
            }
        }

        [HttpPost("completion")]
        public ActionResult<string> GetCompletionAPI([FromBody] Model model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                Response = _chatGPTService.GetAPIResponse(model.requestBody).Result.Item1;
                StatusCode = _chatGPTService.GetAPIResponse(model.requestBody).Result.Item2;
                switch (StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Response.choices[0].message.content;
                    default:
                        return new StatusCodeResult((int)StatusCode);
                }
            }
        }

        public class Model
        {
            [Required]
            public string requestBody { get; set; }
        }
    }
}
