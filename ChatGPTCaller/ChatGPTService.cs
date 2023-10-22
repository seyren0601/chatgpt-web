using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Http;

namespace ChatGPTCaller
{
	public class ChatGPTService
	{
		string _APIKey = File.ReadAllText("D:\\OneDrive - nhg.vn\\CNTT_HongBang\\NCKH - Eureka\\ChatGPT_API\\Bearer Token.txt");
		string _prompt { get; set; }
		string _apiUrl = "https://api.openai.com/v1/chat/completions";
		public async Task<APIResponse> GetAPIResponse(string request)
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

			if(response.IsSuccessStatusCode)
			{
				string jsonResponse = response.Content.ReadAsStringAsync().Result;
				//string responseData = JsonConvert.DeserializeObject<string>(jsonResponse);

				APIResponse? aPIResponse = JsonConvert.DeserializeObject<APIResponse>(jsonResponse);
				return aPIResponse;
			}
			else
			{
				return null;
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

	public class APIResponse
	{
		public string? ID { get; set; }
		public string? obj { get; set; }
		public long created { get; set; }
		public string? model { get; set; }
		public List<Choice>? choices { get; set; }
		public Usage usage { get; set; }
	}

	public class Choice
	{
		public int index { get; set; }
		public Message message { get; set; }
		public string finish_reason { get; set; }
	}

	public class Message
	{
		public string role { get; set; }
		public string content { get; set; }
	}

	public class Usage
	{
		public int prompt_tokens { get; set; }
		public int completion_tokens { get; set; }
		public int total_tokens { get; set; }
	}
}
