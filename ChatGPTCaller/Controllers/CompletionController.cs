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
using Newtonsoft.Json;

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

        [HttpGet("completion/test")]
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
        public ActionResult<ChatGPT_API_Response.APIResponse> GetCompletionAPI([FromBody]PromptRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else
            {
                var result = _chatGPTService.GetAPIResponse(request).Result;
                Response = result.Item1;
                StatusCode = result.Item2;
                switch (StatusCode)
                {
                    case HttpStatusCode.OK:
                        return Response;
                    default:
                        return new StatusCodeResult((int)StatusCode);
                }
            }
        }
    }
}
