using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ChatGPTCaller.Controllers.SinhVien
{
    [Route("sinhvien")]
    [ApiController]
    public class SinhVienController : Controller
    {
        UserInfo SinhVien;
        GetService SV;
        public SinhVienController(GetService getService)
        {
            SV = getService;
        }

        [HttpGet("getSV")]
        public void GetAll()
        {
            DataTable sinhviens = SV.GetSinhViens();
            string json = SV.DataTableToJSONWithJSONNet(sinhviens);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }
        [HttpGet("getSV/{id}")]
        public void GetMotSinhVien(int id)
        {
            DataTable sinhviens = SV.GetMotSinhVienID(id);
            string json = SV.DataTableToJSONWithJSONNet(sinhviens);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }
    }
}
