using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.Data;

namespace ChatGPTCaller.Services.ThongKe
{
    public class ThongKeService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public ThongKeService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }

        public DataTable GetTruyVan()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM THONGKETRUYVAN";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public DataTable GetTruyVanCauHoi()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT TruyVanText, COUNT(*) AS TotalCount\r\nFROM THONGKETRUYVAN\r\nGROUP BY TruyVanText\r\nORDER BY TruyVanText;";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }

        public DataTable GetTruyVanTheoNgay(string Ngay)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT T.TruyVanId,U.full_name,U.email,T.TruyVanText,T.TraLoiText,T.ThoiGian FROM THONGKETRUYVAN as T inner join user_info as U on T.id=U.id WHERE ThoiGian='{Ngay}'";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public UpdateRespond XoaTruyVan(int id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"DELETE FROM THONGKETRUYVAN" +
              $" WHERE TruyVanId = '{id}'";

            try
            {
                int affected = _dbContext.ExecuteNonQueryCommand(sql);
                response.ErrorMessage = "";
                response.UpdateResult = true;
                return response;
            }
            catch (MySqlException e)
            {
                response.ErrorMessage = e.Message;
                response.UpdateResult = false;
                return response;
            }

        }
        public UpdateRespond ThemTruyVan(ThongKeTruyVan thongKeTruyVan)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"INSERT INTO THONGKETRUYVAN (id,TruyVanText,TraLoiText,ThoiGian) VALUES (" +
                         $"'{thongKeTruyVan.Id}', " +
                         $"'{thongKeTruyVan.TruyVanText}', " +
                         $"'{thongKeTruyVan.TraLoiText}', " +
                         $"'{thongKeTruyVan.ThoiGian}')";

            try
            {
                int affected = _dbContext.ExecuteNonQueryCommand(sql);
                response.ErrorMessage = "";
                response.UpdateResult = true;
                return response;
            }
            catch (MySqlException e)
            {
                response.ErrorMessage = e.Message;
                response.UpdateResult = false;
                return response;
            }

        }
        //thong ke dang nhap
        public DataTable GetDangNhap()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT T.DangNhapId,U.full_name,U.email,T.LoginTime FROM THONGKEDANGNHAP as T inner join user_info as U on T.id=U.id;";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public DataTable GetDangNhapTheoNgay(string Ngay)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT U.full_name, U.email, COUNT(T.DangNhapId) AS login_count, MIN(T.LoginTime) AS login_day FROM THONGKEDANGNHAP AS T INNER JOIN user_info AS U ON T.id = U.id  WHERE LoginTime='{Ngay}' GROUP BY U.full_name, U.email";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public UpdateRespond XoaDangNhap(int id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"DELETE FROM THONGKEDANGNHAP" +
              $" WHERE DangNhapId = '{id}'";

            try
            {
                int affected = _dbContext.ExecuteNonQueryCommand(sql);
                response.ErrorMessage = "";
                response.UpdateResult = true;
                return response;
            }
            catch (MySqlException e)
            {
                response.ErrorMessage = e.Message;
                response.UpdateResult = false;
                return response;
            }

        }
        public UpdateRespond ThemDangNhap(ThongKeDangNhap thongKeDangNhap)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"INSERT INTO THONGKEDANGNHAP (id,LoginTime) VALUES (" +
                         $"'{thongKeDangNhap.Id}', " +
                         $"'{thongKeDangNhap.LoginTime}')";

            try
            {
                int affected = _dbContext.ExecuteNonQueryCommand(sql);
                response.ErrorMessage = "";
                response.UpdateResult = true;
                return response;
            }
            catch (MySqlException e)
            {
                response.ErrorMessage = e.Message;
                response.UpdateResult = false;
                return response;
            }

        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
