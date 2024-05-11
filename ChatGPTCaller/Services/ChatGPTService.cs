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
using System.Xml;

namespace ChatGPTCaller.Services
{
    public class ChatGPTService
    {
        string _APIKey = File.ReadAllText("Bearer Token.txt"); // Phải tự tạo file này với nội dung là API Key của ChatGPT
        string _prompt { get; set; }
        string _apiUrl = "https://api.openai.com/v1/chat/completions";
        ArrayList Messages = new ArrayList();
        DbContext db = new DbContext();
        ConversationService _conversationService = new ConversationService();
        public async Task<(ChatGPT_API_Response.APIResponse, HttpStatusCode)> GetAPIResponse(PromptRequest request)
        {
            _prompt = request.message.content;
            Messages.Add(new
            {
                role = "system",
                content = "Bạn là một giảng viên đại học giải thích về thuật toán được người dùng nhập vào trong môn học cấu trúc dữ liệu và giải thuật cho người mới học lập trình." +
                            "Dựa trên thông tin người dùng nhập vào, bạn sẽ trả lời theo một trong 2 cách sau:\n" +
                            "-Người dùng hỏi về khái niệm của thuật toán hoặc cấu trúc dữ liệu và giải thuật nào đó:bạn sẽ trả về khái niệm, ví dụ và đường dẫn youtube của thuật toán hoặc cấu trúc dữ liệu đó mà tôi đã đưa cho bạn trong Tool Call.\n" +
                            
                            "-Người dùng hỏi về thao tác xử lý đối với cấu trúc dữ liệu và giải thuật nào đó:bạn trả về kết quả như sau \"3 đến 4 dòng đầu sẽ giải thích về cách thức hoạt động và quy trình xử lý của thao tác đó, đồng thời trả về mã nguồn C++\"\n"

                //"Lưu ý khi trả về kết quả, ," +
                //"ví dụ:\n<concept>\nViết phần khái niệm của bạn trong đây\n</concept>\n<example>\nViết phần ví dụ của bạn trong đây\n</example>"
            });
            Messages.AddRange(_conversationService.GetConversation(request));

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
                    { "get_link_about_sort", Functions.get_link_about_sort },
                    { "get_link_about_search", Functions.get_link_about_search },
                    { "get_link_about_linkedlist", Functions.get_link_about_linkedlist },
                    { "get_link_about_stack", Functions.get_link_about_stack },
                    { "get_link_about_queue", Functions.get_link_about_queue },
                    { "get_link_about_hash", Functions.get_link_about_hash },
                    { "get_link_about_DFS", Functions.get_link_about_DFS },
                    { "get_link_about_BFS", Functions.get_link_about_BFS },
                    { "get_link_about_binary_tree", Functions.get_link_about_binary_tree },
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
                    else if(tool_call.function.name == "get_link_about_search")
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
                    else if(tool_call.function.name =="get_link_about_queue")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var queue_type = function_args["queue_type"];
                        var function_response = function_to_call(queue_type);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id = tool_call.id,
                            name = function_name,
                        });
                    }
                    else if(tool_call.function.name == "get_link_about_stack")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var stack_type = function_args["stack_type"];
                        var function_response = function_to_call(stack_type);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id = tool_call.id,
                            name = function_name,
                        });
                    }
                    else if(tool_call.function.name == "get_link_about_hash")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var hash_process = function_args["hash_process"];
                        var function_response = function_to_call(hash_process);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id =tool_call.id,
                            name = function_name,
                        });
                    }
                    else if (tool_call.function.name == "get_link_about_DFS")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var DFS_process = function_args["DFS_process"];
                        var function_response = function_to_call(DFS_process);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id = tool_call.id,
                            name = function_name,
                        });
                    }
                    else if (tool_call.function.name == "get_link_about_BFS")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var BFS_process = function_args["BFS_process"];
                        var function_response = function_to_call(BFS_process);
                        Messages.Add(new
                        {
                            content = function_response,
                            role = "tool",
                            tool_call_id = tool_call.id,
                            name = function_name,
                        });
                    }
                    else if (tool_call.function.name == "get_link_about_binary_tree")
                    {
                        var function_name = tool_call.function.name;
                        var function_to_call = available_functions[function_name];
                        var function_args = JsonConvert.DeserializeObject<Dictionary<string, string>>(tool_call.function.arguments);
                        var BinaryTree_process = function_args["BinaryTree_process"];
                        var function_response = function_to_call(BinaryTree_process);
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
                        var linkedlist_type = function_args["linkedlist_type"];
                        var function_response = function_to_call(linkedlist_type);
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
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                _conversationService.RecordConversation(request, aPIResponse);
            }

            if (aPIResponse.choices != null
                && aPIResponse.choices[0].message.content.Contains("<concept>") 
                && aPIResponse.choices[0].message.content.Contains("<example>"))
            {
                string responseContent = "<answer>" + aPIResponse.choices[0].message.content + "</answer>";
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(responseContent);
                XmlNode concept = xml.DocumentElement.SelectSingleNode("/answer/concept");
                XmlNode example = xml.DocumentElement.SelectSingleNode("/answer/example");
                string conceptString = "[Khái niệm]\n";
                string exampleString = "\n\n[Ví dụ]\n";
                conceptString += concept.InnerText;
                exampleString += example.InnerText;
                aPIResponse.choices[0].message.content = conceptString + exampleString;
            }
            return (aPIResponse, response.StatusCode);
        }

        private async Task<HttpResponseMessage> PostRequest1()
        {
            using (HttpClient client = new HttpClient())
            {
                Messages.Add(new
                {
                    role = "user",
                    content = _prompt
                });
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_APIKey}");

                var requestBody = new
                {
                    model = "ft:gpt-3.5-turbo-0613:personal::8hjFRloj",
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
                                            @enum = new [] {"merge sort", "quick sort", "bubble sort", "insertion sort" },
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
                                            description = "The search algorithm, eg. Binary search, Linear search"
                                        }
                                    },
                                    required = new[] { "search_type" }
                                }
                            },
                        },
                        new
                        {
                            type = "function",
                            function = new
                            {
                                name = "get_link_about_linkedlist",
                                description = "Get youtube videos about linked list",
                                parameters = new
                                {
                                    type = "object",
                                    properties = new
                                    {
                                        linkedlist_type = new
                                        {
                                            type = "string",
                                            @enum = new [] {"single", "double", "circular" },
                                            description = "The linked list data structure, eg. Single linked list, Double linked list, Circular linked list"
                                        }
                                    },
                                    required = new[] { "linkedlist_type" }
                                }
                            },
                        },
                        new
                        {
                            type = "function",
                            function = new
                            {
                                name = "get_link_about_queue",
                                description = "Get youtube videos about queue",
                                parameters = new
                                {
                                    type = "object",
                                    properties = new
                                    {
                                        linkedlist_type = new
                                        {
                                            type = "string",
                                            @enum = new [] {"priority", "queue" },
                                            description = "The queue data structure, eg. Priority queue, normal queue"
                                        }
                                    },
                                    required = new[] { "queue_type" }
                                }
                            },
                        },
                        new
                        {
                            type="function",
                            function = new
                            {
                                name = "get_link_about_stack",
                                description = "Get youtube videos about stack",
                                parameters = new
                                {
                                    type ="object",
                                    properties = new
                                    {
                                        stack_type = new
                                        {
                                            type="string",
                                            @enum = new[] {"stack"},
                                            description = "The stack data structure"
                                        }
                                    },
                                    required = new[] {"stack_type"}
                                }
                            },
                        },
                        new
                        {
                            type="function",
                            function = new
                            {
                                name = "get_link_about_hash",
                                description = "Get youtube videos about hash table",
                                parameters = new
                                {
                                    type ="object",
                                    properties = new
                                    {
                                        hash_process = new
                                        {
                                            type="string",
                                            @enum = new[] {"hash table"},
                                            description = "The hash table algorithm"
                                        }
                                    },
                                    required = new[] {"hash_process"}
                                }
                            },
                        },
                        new
                        {
                            type="function",
                            function = new
                            {
                                name = "get_link_about_DFS",
                                description = "Get youtube videos about Depth First Search",
                                parameters = new
                                {
                                    type ="object",
                                    properties = new
                                    {
                                        DFS_process = new
                                        {
                                            type="string",
                                            @enum = new[] {"Depth First Search"},
                                            description = "The Depth First Search algorithm"
                                        }
                                    },
                                    required = new[] {"DFS_process"}
                                }
                            },
                        },
                        new
                        {
                            type="function",
                            function = new
                            {
                                name = "get_link_about_BFS",
                                description = "Get youtube videos about Breadth First Search",
                                parameters = new
                                {
                                    type ="object",
                                    properties = new
                                    {
                                        BFS_process = new
                                        {
                                            type="string",
                                            @enum = new[] {"Breadth First Search"},
                                            description = "The Breadth First Search algorithm"
                                        }
                                    },
                                    required = new[] {"BFS_process"}
                                }
                            },
                        },
                        new
                        {
                            type="function",
                            function = new
                            {
                                name = "get_link_about_binary_tree",
                                description = "Get youtube videos about binary tree",
                                parameters = new
                                {
                                    type ="object",
                                    properties = new
                                    {
                                        BinaryTree_process = new
                                        {
                                            type="string",
                                            @enum = new[] {"binary tree"},
                                            description = "The binary tree data structure"
                                        }
                                    },
                                    required = new[] {"BinaryTree_process"}
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
                    model = "ft:gpt-3.5-turbo-0613:personal::9BNpkE3C",
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
