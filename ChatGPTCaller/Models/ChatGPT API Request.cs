using System.Collections.Generic;

namespace ChatGPTCaller.Models
{
    public class ChatGPT_API_Request
    {
        public class APIRequest
        {
            public string model { get; set; }
            public List<ChatGPT_API_Response.Message> messages { get; set; }
            public APIRequest(string model, List<ChatGPT_API_Response.Message> messages)
            {
                this.model = model;
                this.messages = messages;
            }
        }
    }
}
