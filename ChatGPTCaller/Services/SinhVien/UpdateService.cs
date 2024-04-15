using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using System;
using System.IO;
namespace ChatGPTCaller.Services.SinhVien
{
    public class UpdateService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
        DbContext _dbContext;
        public UpdateService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
            _webHostEnvironment = webHostEnvironment;
        }
        public UpdateRespond UpdateUser(UserInfo user)
        {
            UpdateRespond response = new UpdateRespond();

            // Save the avatar image to wwwroot/avatars directory
            if (user.Avatar != null && user.Avatar.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "avatars");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);  // Create the directory if it doesn't exist
                }
                var uniqueFileName = user.email + "_" + Path.GetFileName(user.Avatar.FileName);
               var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                var filePath1 = Path.Combine("/avatars/", uniqueFileName);


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    user.Avatar.CopyTo(fileStream);
                }

                // Store the file path in the user object
                user.picture = filePath1;
            }

            // Update user information in the database
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
                $"picture = '{user.picture}', " + 
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