using ChatGPTCaller.Models;
using ChatGPTCaller.Services.MonHocMoi;
using ChatGPTCaller.Services.ThongKe;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ChatGPTCaller.Controllers.ThongKe
{
    [Route("admin")]
    [ApiController]
    public class ThongKeController : ControllerBase
    {

        ThongKeTruyVan ThongKeTruyVan;
        ThongKeService _thongKeService;
        UpdateRespond response;
        public ThongKeController(ThongKeService thongKeService)
        {
            _thongKeService = thongKeService;
        }
      
        [HttpPost("themtruyvan")]
        public ActionResult<UpdateRespond> PostchuongResult([FromBody] ThongKeTruyVan thongKeTruyVan)
        {
            thongKeTruyVan.ThoiGian=DateTime.Now;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _thongKeService.ThemTruyVan(thongKeTruyVan);
                return response;
            }
        }
        
    }
}
