using ChatGPTCaller.Models;
using ChatGPTCaller.Services.Admin;
using ChatGPTCaller.Services.MonHocMoi;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;


namespace ChatGPTCaller.Controllers.MonHocNay
{
    [Route("monhoc")]
    [ApiController]
    public class MonHocController : Controller
    {
        MonHoc monhoc;
        MonHocService _monHocService;
        UpdateRespond response;
        public MonHocController( MonHocService monHocService)
        {
           _monHocService = monHocService;
        }
        [HttpGet("getMonHoc")]
        public void GetAll()
        {
            DataTable Monhoc = _monHocService.GetBook();
            string json = _monHocService.DataTableToJSONWithJSONNet(Monhoc);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }

        [HttpGet("getMonHoc/{id}")]
        public void GetMotMonHoc(string id)
        {
            DataTable Monhoc = _monHocService.GetMotBook(id);
            string json = _monHocService.DataTableToJSONWithJSONNet(Monhoc);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }

        [HttpPost("delete/{id}")]
        public ActionResult<UpdateRespond> PostDeleteResult(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _monHocService.XoaBook(id);
                return response;
            }
        }

        [HttpPost("update/{id}")]
        public ActionResult<UpdateRespond> PostupdateResult(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _monHocService.XoaBook(id);
                return response;
            }
        }

        [HttpGet("getChuong/{id}")]
        public void GetChuong(string id)
        {
            DataTable CHUONG= _monHocService.GetChuong(id);
            string json = _monHocService.DataTableToJSONWithJSONNet(CHUONG);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }

        [HttpPost("deleteChuong/{id}")]
        public ActionResult<UpdateRespond> ChuongDeleteResult(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _monHocService.XoaChuong(id);
                return response;
            }
        }

        [HttpPost("updatechuong/{id}")]
        public ActionResult<UpdateRespond> ChuongupdateResult(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = _monHocService.XoaChuong(id);
                return response;
            }
        }
    }
}
