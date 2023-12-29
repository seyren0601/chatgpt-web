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

            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            ChatGPT_API_Response.APIResponse aPIResponse = JsonConvert.DeserializeObject<ChatGPT_API_Response.APIResponse>(jsonResponse);
            if(response.StatusCode == HttpStatusCode.OK)
            {
                RecordConversation(request, aPIResponse);
            }
            return (aPIResponse, response.StatusCode);
        }

        private async Task<HttpResponseMessage> PostRequest()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_APIKey}");

                var requestBody = new ChatGPT_API_Request.APIRequest("gpt-3.5-turbo", Messages);

                var jsonRequest = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrl, content);
                return response;
            }
        }
    }
}
