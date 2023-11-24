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
using System.Net;
using static ChatGPTCaller.Services.ConversationService;

namespace ChatGPTCaller.Services
{
    public class ChatGPTService
    {
        string _APIKey = File.ReadAllText("Bearer Token.txt"); // Phải tự tạo file này với nội dung là API Key của ChatGPT
        string _prompt { get; set; }
        string _apiUrl = "https://api.openai.com/v1/chat/completions";
        List<ChatGPT_API_Response.Message> Messages = new List<ChatGPT_API_Response.Message>();
        public async Task<(ChatGPT_API_Response.APIResponse, HttpStatusCode)> GetAPIResponse(PromptRequest request)
        {
            _prompt = request.message.content;
            Messages = GetConversation(request);
            
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

            string jsonResponse = response.Content.ReadAsStringAsync().Result;
            //string responseData = JsonConvert.DeserializeObject<string>(jsonResponse);

            ChatGPT_API_Response.APIResponse aPIResponse = JsonConvert.DeserializeObject<ChatGPT_API_Response.APIResponse>(jsonResponse);
            RecordConversation(request, aPIResponse);
            return (aPIResponse, response.StatusCode);
        }

        private async Task<HttpResponseMessage> PostRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_APIKey}");

                var requestBody = new ChatGPT_API_Request.APIRequest("gpt-3.5-turbo", Messages);
                /*{
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new { role = "system", content = "You are a helpful assistant." },
                        new { role = "user", content = _prompt }
                    }
                };*/

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
