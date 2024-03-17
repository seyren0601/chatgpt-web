using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ChatGPTCaller.Controllers.SinhVien
{
    [Route("getsinhvien")]
    [ApiController]
    public class SinhVienController : Controller
    {
        UserInfo SinhVien;
        GetService SV;
        public SinhVienController(GetService getService)
        {
            SV = getService;
        }

        [HttpGet("request")]
        public void GetAll()
        {
            DataTable sinhviens = SV.GetSinhViens();
            string json = SV.DataTableToJSONWithJSONNet(sinhviens);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }
    }
}
