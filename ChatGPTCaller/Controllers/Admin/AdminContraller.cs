using ChatGPTCaller.Models;
using ChatGPTCaller.Services;
using ChatGPTCaller.Services.Admin;
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
        UpdateRespond response;
        public AdminContraller(GetService getService,AdminService adminService)
        {
            SV = getService;
            AdminService = adminService;
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
