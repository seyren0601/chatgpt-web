﻿using ChatGPTCaller.DAL;
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
        public DataTable GetTruyVanTheoNgay(string Ngay)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT T.TruyVanId,T.id,U.full_name,T.TruyVanText,T.TraLoiText,T.ThoiGian FROM THONGKETRUYVAN as T inner join user_info as U on T.id=U.id WHERE ThoiGian='{Ngay}'";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public UpdateRespond XoaTruyVan(int id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"UPDATE user_info SET " +
              $"isdeleted = true" +
              $" WHERE id = '{id}'";

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
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}
