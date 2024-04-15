using ChatGPTCaller.Models;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Text.Json;

namespace ChatGPTCaller.Controllers.SinhVien
{
    [Route("sinhvien")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly UpdateService _updateService;

        public UpdateController(UpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpPost("update")]
        public ActionResult<UpdateRespond> PostUpdateResult([FromForm] IFormCollection formData)
        {
            var user = new UserInfo();

            // Extract JSON data from formData
            var userInfoJson = formData["json"];
            if (!string.IsNullOrWhiteSpace(userInfoJson))
            {
                user = JsonSerializer.Deserialize<UserInfo>(userInfoJson);
            }

            // Extract file from formData
            if (formData.Files.Any())
            {
                user.Avatar = formData.Files.First();
            }

            if (user == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = _updateService.UpdateUser(user);
            return response;
        }
    }
}
