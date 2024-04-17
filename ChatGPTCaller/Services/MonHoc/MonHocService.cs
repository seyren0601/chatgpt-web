using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Data;

namespace ChatGPTCaller.Services.MonHocMoi
{
    public class MonHocService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public MonHocService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(_configuration);
        }

        public DataTable GetAllBook()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM MONHOC";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public DataTable GetMotBook(string id)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM MONHOC WHERE IdMonhoc = '{id}'";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public UpdateRespond XoaBook(string id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"DELETE FROM MONHOC" +
              $" WHERE IdMonhoc = '{id}'";

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
        public UpdateRespond SuaBook(MonHoc monhoc, string id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"UPDATE MONHOC SET " +
              $"TitleMonhoc = '{monhoc.TitleMonhoc}', " +
              $"ContentMonhoc = '{monhoc.ContentMonhoc} ' " +
              $" WHERE IdMonhoc = '{id}'";

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
        public UpdateRespond ThemBook(MonHoc monhoc)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"INSERT INTO MONHOC VALUES (" +
                         $"'{monhoc.IdMonhoc}', " +
                         $"'{monhoc.TitleMonhoc}', " +
                         $"'{monhoc.ContentMonhoc}')";

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
        public DataTable GetAllChuong()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM CHUONG ";
            resultTable = _dbContext.ExecuteQueryCommand(sql);
            return resultTable;
        }

        public DataTable GetChuong(string id)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM CHUONG WHERE IdMonhoc='{id}'";
            resultTable = _dbContext.ExecuteQueryCommand(sql);
            return resultTable;
        }
        public DataTable GetmoiChuong(string id)
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM CHUONG WHERE Id='{id}'";
            resultTable = _dbContext.ExecuteQueryCommand(sql);
            return resultTable;
        }
        public UpdateRespond XoaChuong(string id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"DELETE FROM CHUONG" +
              $" WHERE Id = '{id}'";

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
        public UpdateRespond SuaChuong(Chuong chuong, string id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"UPDATE CHUONG SET " +
              $"Title = '{chuong.Title}', " +
              $"IdMonhoc = '{chuong.IdMonhoc} ', " +
              $"ParentId = '{chuong.ParentId} ' " +
              $" WHERE Id = '{id}'";

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
        public UpdateRespond ThemChuong(Chuong chuong)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"INSERT INTO CHUONG (Id,Title,IdMonhoc,ParentId) VALUES (" +
                         $"'{chuong.Id}', " +
                         $"'{chuong.Title}', " +
                         $"'{chuong.IdMonhoc}', " +
                         $"'{chuong.ParentId}')";

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
