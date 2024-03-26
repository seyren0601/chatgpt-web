using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Data;
using System;
using Microsoft.Extensions.Configuration;
using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;

namespace ChatGPTCaller.Services.SinhVien
{
    public class GetService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public GetService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }

        public DataTable GetSinhViens()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM user_info";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public DataTable GetMotSinhVien(string email)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM user_info WHERE email = '{email}'";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
