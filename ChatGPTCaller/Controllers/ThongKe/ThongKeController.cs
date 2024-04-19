using ChatGPTCaller.Models;
using ChatGPTCaller.Services.Admin;
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
        public void Getalltruyvan()
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
        [HttpPost("deletetruyvan/{id}")]
        public ActionResult<UpdateRespond> PostDeleteResult(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _thongKeService.XoaTruyVan(id);
                return response;
            }
        }
        //thong ke dang nhap

        [HttpGet("getdangnhap")]
        public void Getalldangnhap()
        {
            DataTable truyvan = _thongKeService.GetDangNhap();
            string json = _thongKeService.DataTableToJSONWithJSONNet(truyvan);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }
        [HttpGet("getdangnhap/{ngay}")]
        public void Getmotdangnhap(string ngay)
        {
            DataTable truyvan = _thongKeService.GetDangNhapTheoNgay(ngay);
            string json = _thongKeService.DataTableToJSONWithJSONNet(truyvan);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }

        [HttpPost("themdangNhap")]
        public ActionResult<UpdateRespond> PostdangnhapResult([FromBody] ThongKeDangNhap thongKeDangNhap)
        {
            thongKeDangNhap.LoginTime = DateTime.Now.ToString("yyyy-MM-dd");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _thongKeService.ThemDangNhap(thongKeDangNhap);
                return response;
            }
        }
        [HttpPost("deletedangnhap/{id}")]
        public ActionResult<UpdateRespond> DangNhapDeleteResult(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _thongKeService.XoaDangNhap(id);
                return response;
            }
        }


    }
}
