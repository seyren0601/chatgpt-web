using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;

namespace ChatGPTCaller.Services
{
    public class RegisterService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public RegisterService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }
        
        public RegisterResponse RegisterUser(User user)
        {
            RegisterResponse response = new RegisterResponse();
            UserInfo userInfo = new UserInfo(user.mssv, user.full_name, user.gender, user.birthday, user.email,
            user.address, user.nationality, user.religion, user.entry_date, user.faculty, user.major, user.class_sv, user.password);
            string sql = $"INSERT INTO user_info VALUES (NULL,NULL,'{user.full_name}',NULL,NULL,'{userInfo.email}',NULL,NULL,NULL,NULL,NULL,NULL,NULL,'{userInfo.GetHashValue()}', '{Convert.ToBase64String(userInfo.hashSalt)}')";
            try
            {
                int affected = _dbContext.ExecuteNonQueryCommand(sql);
                response.ErrorMessage = "";
                response.RegisterResult = true;
                return response;
            }
            catch(MySqlException e)
            {
                response.ErrorMessage = e.Message;
                response.RegisterResult = false;
                return response;
            }
            
        }
    }
}
