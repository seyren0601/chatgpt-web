using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using MySqlConnector;
using System;
using static ChatGPTCaller.DAL.DbContext;

namespace ChatGPTCaller.Services
{
    public class RegisterService
    {
        public RegisterResponse RegisterUser(User user)
        {
            RegisterResponse response = new RegisterResponse();
            UserInfo userInfo = new UserInfo(user.email, user.password);
            string sql = $"INSERT INTO user_info VALUES (NULL, '{userInfo.email}', '{userInfo.GetHashValue()}', '{Convert.ToBase64String(userInfo.hashSalt)}')";
            try
            {
                int affected = ExecuteNonQueryCommand(sql);
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
