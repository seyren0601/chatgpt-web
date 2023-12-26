using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MySqlConnector;
using System;
using System.Data;
using System.Data.Common;

namespace ChatGPTCaller.DAL
{
    public class DbContext
    {
        string connectionString;
        private MySqlConnection _connection;
        public DbContext(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("userdb");
            _connection = new MySqlConnection(connectionString);
        }
        public DbContext() { }

        public int ExecuteNonQueryCommand(string sql)
        {
            _connection.Open();

            MySqlCommand cmd = new MySqlCommand() 
            { 
                CommandText = sql,
                CommandType = CommandType.Text,
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
                Console.WriteLine(_connection.ConnectionString);
                throw (e);
            }
        }

        public DataTable ExecuteQueryCommand(string sql)
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
