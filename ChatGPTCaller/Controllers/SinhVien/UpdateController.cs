using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTCaller.Controllers.SinhVien
{
    [Route("sinhvien")]
    [ApiController]
    public class UpdateController : ControllerBase
    {
        private readonly UpdateService _updateService;
        UpdateRespond Response;
        public UpdateController(UpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpPost("update")]
        public ActionResult<UpdateRespond> PostUpdateResult([FromBody] UserInfo user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                Response = _updateService.UpdateUser(user);
                return Response;
            }
        }
    }
}
