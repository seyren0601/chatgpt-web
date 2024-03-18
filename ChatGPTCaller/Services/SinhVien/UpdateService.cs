using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
namespace ChatGPTCaller.Services.SinhVien
{
    public class UpdateService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public UpdateService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }
        public UpdateRespond UpdateUser(UserInfo user)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"UPDATE user_info SET " +
              $"mssv = '{user.mssv}', " +
              $"gender = '{user.gender}', " +
              $"birthday = '{user.birthday.ToString("yyyy-MM-dd")}', " +
              $"faculty = '{user.faculty}', " +
              $"major = '{user.major}', " +
              $"nationality = '{user.nationality}', " +
              $"religion = '{user.religion}', " +
              $"idcard = '{user.idcard}', " +
              $"dateofissue = '{user.dateofissue.ToString("yyyy-MM-dd")}', " +
              $"placeofissue = '{user.placeofissue}', " +
              $"myphone = '{user.myphone}', " +
              $"parentphone = '{user.parentphone}', " +
              $"address = '{user.address}', " +
              $"aboutstudent = '{user.aboutstudent}' " +
              $"WHERE email = '{user.email}'";
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
