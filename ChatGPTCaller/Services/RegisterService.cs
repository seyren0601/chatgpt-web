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
            UserInfo userInfo = new UserInfo(user);
            RegisterResponse response = new RegisterResponse();
            string sql = $"INSERT INTO user_info(full_name, email, hashed_pw, salt) " +
                         $"VALUES ('{userInfo.full_name}','{userInfo.email}','{userInfo.GetHashValue()}', '{Convert.ToBase64String(userInfo.hashSalt)}')";
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
