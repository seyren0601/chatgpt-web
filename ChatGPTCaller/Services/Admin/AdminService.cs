using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;

using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace ChatGPTCaller.Services.Admin
{
    public class AdminService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public AdminService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }

        public DataTable GetSinhVienAdmin()
        {
            DataTable resultTable = new DataTable();
            string sql = $"SELECT * FROM user_info ";
            DataTable dt = _dbContext.ExecuteQueryCommand(sql);
            return dt;
        }
        public UpdateRespond XoaUser(int id)
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
        public UpdateRespond SuaUser(UserInfo user,int id)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"UPDATE user_info SET " +
              $"mssv = '{user.mssv}', " +
              $"gender = '{user.gender}', " +
              $"birthday = '{user.birthday}', " +
              $"faculty = '{user.faculty}', " +
              $"major = '{user.major}', " +
              $"nationality = '{user.nationality}', " +
              $"religion = '{user.religion}', " +
              $"idcard = '{user.idcard}', " +
              $"dateofissue = '{user.dateofissue}', " +
              $"placeofissue = '{user.placeofissue}', " +
              $"myphone = '{user.myphone}', " +
              $"parentphone = '{user.parentphone}', " +
              $"address = '{user.address}', " +
              $"aboutstudent = '{user.aboutstudent}', " +
              $"role ='{user.role} '" +
              $"WHERE id = '{id}'";

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
    }
}
