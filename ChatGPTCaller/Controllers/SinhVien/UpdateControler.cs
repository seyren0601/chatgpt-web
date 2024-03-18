using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatGPTCaller.Controllers.SinhVien
{
    [Route("update")]
    [ApiController]
    public class UpdateControler : ControllerBase
    {
        private readonly UpdateService _updateService;
        UpdateRespond Response;
        public UpdateControler(UpdateService updateService)
        {
            _updateService = updateService;
        }

        [HttpPost("sinhvien")]
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
