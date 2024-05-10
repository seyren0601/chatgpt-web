using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using MySqlConnector;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.Generic;

namespace ChatGPTCaller.Services
{
    public class LoginService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public LoginService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }

        public LoginResponse LoginUser(string email, string password)
        {
            string sql = $"SELECT * FROM user_info WHERE email = '{email}'";
            try
            {
                DataTable dt = _dbContext.ExecuteQueryCommand(sql);
                if (dt.Rows.Count == 0)
                {
                    return new LoginResponse()
                    {
                        LogInResult = false,
                        ErrorMessage = "Không tìm thấy user"
                    };
                }

                DataRow user_row = dt.Rows[0];
                object db_h_pass_obj = user_row["hashed_pw"];
                object salt_obj = user_row["salt"];

                if (db_h_pass_obj == DBNull.Value || salt_obj == DBNull.Value)
                {
                    return new LoginResponse()
                    {
                        LogInResult = false,
                        ErrorMessage = "Invalid password data in database"
                    };
                }

                string db_h_pass = (string)db_h_pass_obj;
                byte[] salt = Convert.FromBase64String((string)salt_obj);
                string h_pass = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                    password: password!,
                                    salt: salt,
                                    prf: KeyDerivationPrf.HMACSHA256,
                                    iterationCount: 100000,
                                    numBytesRequested: 256 / 8));
                if (h_pass == db_h_pass)
                {
                    return new LoginResponse()
                    {
                        LogInResult = true,
                        UserID = Convert.ToInt32(user_row["id"]),
                        UserRole = Convert.ToInt32(user_row["id"]) == 1 ? "admin" : "user"
                    };
                }
                else
                {
                    return new LoginResponse()
                    {
                        LogInResult = false,
                        ErrorMessage = "Sai mật khẩu"
                    };
                }
            }
            catch (Exception ex)
            {
                return new LoginResponse()
                {
                    LogInResult = false,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
