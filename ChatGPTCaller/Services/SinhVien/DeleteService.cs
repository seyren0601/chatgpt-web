using ChatGPTCaller.DAL;
using ChatGPTCaller.Models;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace ChatGPTCaller.Services.SinhVien
{
    public class DeleteService
    {
        private readonly IConfiguration _configuration;
        DbContext _dbContext;
        public DeleteService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = new DbContext(configuration);
        }
        public UpdateRespond DeleteUser(UserInfo user)
        {
            UpdateRespond response = new UpdateRespond();
            string sql = $"UPDATE user_info SET " +
              $"isdeleted = '{user.isdeleted}' " +
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
