using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;

namespace ChatGPTCaller.Models
{
    public class ChatGPT_API_Request
    {
        public class APIRequest:ChatGPT_API_Response.Message
        {
            public string model { get; set; }
            public ArrayList messages { get; set; }
            public List<Tool> tools { get; set; }
            public string tool_choice { get; set; }
            public APIRequest(string model, ArrayList messages)
            {
                this.model = model;
                this.messages = messages;
            }
            public APIRequest(string model, ArrayList messages, List<Tool> tools, string tool_choice) : this(model, messages)
            {
                this.tools = tools;
                this.tool_choice = tool_choice;
            }
            public APIRequest() { }
        }

        public class Tool
        {
            public string type { get; set; }
            public Function function { get; set; }
        }

        public class Function
        {
            public string name { get; set; }
            public string description { get; set; }
            public Parameters parameters { get; set; }
        }

        public class Parameters
        {
            public string type { get; set; }
            public object properties { get; set; }
            public string[] required { get; set; }
        }

        public class SortType
        {
            public string type { get; set; }
            public string description { get; set; }
            public SortType(string type, string description)
            {
                this.type = type;
                this.description = description;
            }
            public SortType() { }
        }
    }
}
