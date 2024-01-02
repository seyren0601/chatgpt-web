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

        internal string get_link_about_sort(string sort_type)
        {
            sort_type = sort_type.ToLower();
            if (sort_type.Contains("quick sort"))
            {
                var quickSort = new
                {
                    sort_type = "Quick sort",
                    link = "https://youtu.be/Gyj8fd4DBpc?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_"
                };
                return JsonConvert.SerializeObject(quickSort);
            }
            if (sort_type.Contains("bubble sort"))
            {
                var bubbleSort = new
                {
                    sort_type = "Bubble sort",
                    link = "https://www.youtube.com/watch?v=xli_FI7CuzA&pp=ygULYnViYmxlIHNvcnQ%3D"
                };
                return JsonConvert.SerializeObject(bubbleSort);
            }
            if (sort_type.Contains("insertion sort"))
            {
                var insertionSort = new
                {
                    sort_type = "Insertion sort",
                    link = "https://www.youtube.com/watch?v=JU767SDMDvA&pp=ygUOaW5zZXJ0aW9uIHNvcnQ%3D"
                };
                return JsonConvert.SerializeObject(insertionSort);
            }
            if (sort_type.Contains("merge sort"))
            {
                var mergeSort = new
                {
                    sort_type = "Merge sort",
                    link = "https://www.youtube.com/watch?v=4VqmGXwpLqc&pp=ygUKbWVyZ2Ugc29ydA%3D%3D"
                };
                return JsonConvert.SerializeObject(mergeSort);
            }
            return "";
        }

        internal string get_link_about_search(string search_type)
        {
            search_type = search_type.ToLower();
            if (search_type.Contains("linear search"))
            {
                var linearSearch = new
                {
                    search_type = "Linear search",
                    link = "https://www.youtube.com/watch?v=YvAosi_pZ8w&list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&index=3&pp=iAQB"
                };
                return JsonConvert.SerializeObject(linearSearch);
            }
            if (search_type.Contains("binary search"))
            {
                var binarySearch = new
                {
                    search_type = "Binary search",
                    link = "https://youtu.be/YvAosi_pZ8w?list=PLrcfrbKhqmAWrIoc47jVx1eEzS9gJUdg_&t=2365"
                };
                return JsonConvert.SerializeObject(binarySearch);
            }
            return "";
        }
    }
}
