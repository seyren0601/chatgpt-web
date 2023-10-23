using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using ChatGPTCaller.Models;

namespace ChatGPTCaller.Services
{
    public class ChatGPTService
    {
        string _APIKey = File.ReadAllText("Bearer Token.txt");
        string _prompt { get; set; }
        string _apiUrl = "https://api.openai.com/v1/chat/completions";
        public async Task<ChatGPT_API_Response.APIResponse> GetAPIResponse(string request)
        {
            _prompt = request;

            HttpResponseMessage response = await PostRequest();

            /*if (response.IsSuccessStatusCode)
			{
				string jsonResponse = response.Content.ReadAsStringAsync().Result;
				string responseData = JsonConvert.DeserializeObject<string>(jsonResponse);

				string filePath = "response.json";
				File.WriteAllText(filePath, JsonConvert.SerializeObject(responseData));
				Console.WriteLine("Response saved to response.json");
			}
			else
			{
				Console.WriteLine($"Error: {response.ReasonPhrase}");
			}*/

            //string jsString = File.ReadAllText("response.json");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;
                //string responseData = JsonConvert.DeserializeObject<string>(jsonResponse);

                ChatGPT_API_Response.APIResponse aPIResponse = JsonConvert.DeserializeObject<ChatGPT_API_Response.APIResponse>(jsonResponse);
                return aPIResponse;
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        private async Task<HttpResponseMessage> PostRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_APIKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = _prompt }
                    }
                };

                var jsonRequest = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrl, content);
                return response;
            }
        }
    }



    /*public class Request
	{
		public string? model { get; set; }
		public List<Message> messages { get; set; }
	}*/
}
