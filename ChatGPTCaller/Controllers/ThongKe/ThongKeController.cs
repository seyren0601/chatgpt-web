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
        [HttpGet("gettruyvan")]
        public void Getalltruyvan(string ngay)
        {
            DataTable truyvan = _thongKeService.GetTruyVan();
            string json = _thongKeService.DataTableToJSONWithJSONNet(truyvan);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }
        [HttpGet("gettruyvan/{ngay}")]
        public void Getmottruyvan(string ngay)
        {
            DataTable truyvan = _thongKeService.GetTruyVanTheoNgay(ngay);
            string json = _thongKeService.DataTableToJSONWithJSONNet(truyvan);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }

        [HttpPost("themtruyvan")]
        public ActionResult<UpdateRespond> PostchuongResult([FromBody] ThongKeTruyVan thongKeTruyVan)
        {
            thongKeTruyVan.ThoiGian = DateTime.Now.ToString("yyyy-MM-dd");
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
