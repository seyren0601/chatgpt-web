using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using ChatGPTCaller.Services.Admin;
using ChatGPTCaller.Services.MonHocMoi;
using ChatGPTCaller.Services.SinhVien;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace ChatGPTCaller.Controllers.Admin
{
    [Route("admin")]
    [ApiController]
    public class AdminContraller:Controller
    {
        UserInfo SinhVien;
        GetService SV;
        AdminService AdminService;
        MonHocService _monHocService;
        UpdateRespond response;
        public AdminContraller(GetService getService,AdminService adminService, MonHocService monHocService)
        {
            SV = getService;
            AdminService = adminService;
            _monHocService = monHocService;
        }
        [HttpGet("getSV")]
        public void GetAll()
        {
            DataTable sinhviens = AdminService.GetSinhVienAdmin();
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
        [HttpPost("delete/{id}")]
        public ActionResult<UpdateRespond> PostDeleteResult(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = AdminService.XoaUser(id);
                return response;
            }
        }
        [HttpPost("deletepermanent/{id}")]
        public ActionResult<UpdateRespond> PerDeleteResult(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = AdminService.XoaUserPermanent(id);
                return response;
            }
        }
        [HttpGet("getmotmh/{id}")]
        public void GetMotMonHoc(string id)
        {
            DataTable Monhoc = _monHocService.GetMotBook(id);
            string json = _monHocService.DataTableToJSONWithJSONNet(Monhoc);
            Response.Clear();
            Response.ContentType = "application/json;charset=utf-8";
            Response.WriteAsync(json);
        }
        [HttpPost("update/{id}")]
        public ActionResult<UpdateRespond> PostupdateResult([FromBody] UserInfo user,int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                response = AdminService.SuaUser(user,id);
                return response;
            }
        }

    }
}
