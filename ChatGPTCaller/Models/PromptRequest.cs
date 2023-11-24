using System.ComponentModel.DataAnnotations;
using ChatGPTCaller.Models;

namespace ChatGPTCaller.Models
{
    public class PromptRequest
    {
        public int ConversationID { get; set; }
        public ChatGPT_API_Response.Message message { get; set; }
    }
}
