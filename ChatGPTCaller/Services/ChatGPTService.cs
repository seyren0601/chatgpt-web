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
using System.Linq;
using System.Collections;
using System.Security;
using System.Diagnostics;
using System.Xml.Linq;
using ChatGPTCaller.DAL;

namespace ChatGPTCaller.Services
{
    public class ChatGPTService
    {
        string _APIKey = File.ReadAllText("Bearer Token.txt"); // Phải tự tạo file này với nội dung là API Key của ChatGPT
        string _prompt { get; set; }
        string _apiUrl = "https://api.openai.com/v1/chat/completions";
        ArrayList Messages = new ArrayList();
        DbContext db = new DbContext();
        public async Task<(ChatGPT_API_Response.APIResponse, HttpStatusCode)> GetAPIResponse(PromptRequest request)
        {
            _prompt = request.message.content;
            //Messages = GetConversation(request);
            
            HttpResponseMessage response = await PostRequest1();

            string jsonResponse = response.Content.ReadAsStringAsync().Result;

            ChatGPT_API_Response.APIResponse aPIResponse = JsonConvert.DeserializeObject<ChatGPT_API_Response.APIResponse>(jsonResponse);

            if (aPIResponse.choices != null && aPIResponse.choices[0].finish_reason == "tool_calls")
            {
                var response_message = new
                {
                    content = aPIResponse.choices[0].message.content,
                    role = aPIResponse.choices[0].message.role,
                    tool_calls = new[]
                    {
                        new
                        {
                            id = aPIResponse.choices[0].message.tool_calls[0].id,
                            type = aPIResponse.choices[0].message.tool_calls[0].type,
                            function = aPIResponse.choices[0].message.tool_calls[0].function
                        }
                    }
                };
                List<ChatGPT_API_Response.ToolCall> tool_calls = aPIResponse.choices[0].message.tool_calls;
                var available_functions = new Dictionary<string, Func<string, string>>
                {
                    { "get_link_about_sort", db.get_link_about_sort },
                    { "get_link_about_search", db.get_link_about_search }
                };
                Messages.Add(response_message);
                foreach(var tool_call in tool_calls)
                {
                    if (tool_call.function.name == "get_link_about_sort")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var sort_type = function_args["sort_type"];
                        var function_response = function_to_call(sort_type);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id = tool_call.id,
                            name = function_name,
                        });
                    }
                    else
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var search_type = function_args["search_type"];
                        var function_response = function_to_call(search_type);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id = tool_call.id,
                            name = function_name,
                        });
                    }
                }
                response = await PostRequest2();

                jsonResponse = response.Content.ReadAsStringAsync().Result;
                aPIResponse = JsonConvert.DeserializeObject<ChatGPT_API_Response.APIResponse>(jsonResponse);

                /*if(response.StatusCode == HttpStatusCode.OK)
                {
                    RecordConversation(request, aPIResponse);
                }*/
                return (aPIResponse, response.StatusCode);
            }
            else
            {
                /*if (response.StatusCode == HttpStatusCode.OK)
                {
                    RecordConversation(request, aPIResponse);
                }*/
                return (aPIResponse, response.StatusCode);
            }
        }

        private async Task<HttpResponseMessage> PostRequest1()
        {
            using (HttpClient client = new HttpClient())
            {
                Messages.Add(new
                {
                    role = "system",
                    content = "You are a professor at university and you are teaching the course Data Structures and Algorithms. Do not give source code in answers."
                });
                Messages.Add(new
                {
                    role = "system",
                    content = @"The API completion call can be given a stop sequence like '\nuser'" +
                               "If the AI writes that itself, the characters are recognized and the generation of output is terminated."
                });
                Messages.Add(new
                {
                    role = "user",
                    content = _prompt
                });
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_APIKey}");

                var requestBody = new
                {
                    model = "ft:gpt-3.5-turbo-0613:personal::8g5tg9eN",
                    messages = Messages,
                    tools = new ArrayList
                    {
                        new 
                        {
                            type = "function",
                            function = new
                            {
                                name = "get_link_about_sort",
                                description = "Get materials",
                                parameters = new
                                {
                                    type = "object",
                                    properties = new
                                    {
                                        sort_type = new
                                        {
                                            type = "string",
                                            description = "The sorting algorithm, e.g. Quick sort, Merge sort"
                                        },
                                    },
                                    required = new [] { "sort_type" }
                                }
                            }
                        },
                        new
                        {
                            type = "function",
                            function = new
                            {
                                name = "get_link_about_search",
                                description = "Get link youtube about sorting algorithms",
                                parameters = new
                                {
                                    type = "object",
                                    properties = new
                                    {
                                        search_type = new
                                        {
                                            type = "string",
                                            @enum = new [] {"binary search", "linear search" },
                                            description = "Kiểu thuật giải tìm kiếm"
                                        }
                                    },
                                    required = new[] { "search_type" }
                                }
                            },
                        },
                    },
                    tool_choice = "auto",
                    temperature = 0.2
                };

                //var requestBody = new ChatGPT_API_Request.APIRequest("gpt-3.5-turbo", Messages);

                var jsonRequest = JsonConvert.SerializeObject(requestBody);


                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrl, content);
                return response;
            }
        }

        private async Task<HttpResponseMessage> PostRequest2()
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_APIKey}");

                var requestBody = new
                {
                    model = "ft:gpt-3.5-turbo-0613:personal::8g5tg9eN",
                    messages = Messages,
                };

                //var requestBody = new ChatGPT_API_Request.APIRequest("gpt-3.5-turbo", Messages);

                var jsonRequest = JsonConvert.SerializeObject(requestBody);

                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_apiUrl, content);
                return response;
            }
        }
    }
}
