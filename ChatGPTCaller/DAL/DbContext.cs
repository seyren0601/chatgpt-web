using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace ChatGPTCaller.DAL
{
    static class DbContext
    {
        static string connectionString = "Server=localhost;User ID=root;Password=thuong0909;Database=gpt_user";
        static MySqlConnection _connection = new MySqlConnection(connectionString);

        public static int ExecuteNonQueryCommand(string sql)
        {
            _connection.Open();

            MySqlCommand cmd = new MySqlCommand() 
            { 
                CommandText = sql,
                CommandType = System.Data.CommandType.Text,
                Connection = _connection
            };
            try
            {
                int affected = cmd.ExecuteNonQuery();
                _connection.Close();
                return affected;
            }
            catch(MySqlException e)
            {
                _connection.Close();
                throw (e);
            }
        }

        public static DataTable ExecuteQueryCommand(string sql)
        {
            _connection.Open();

            MySqlCommand cmd = new MySqlCommand()
            {
                CommandText = sql,
                CommandType = CommandType.Text,
                Connection = _connection
            };

            DataTable dt = new DataTable();
            try
            {
                MySqlDataReader dr = cmd.ExecuteReader();
                
                dt.Load(dr);
                _connection.Close();
                return dt;
            }
            catch
            {
                _connection.Close();
                return dt;
            }
        }
    }
}
