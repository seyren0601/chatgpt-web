using Newtonsoft.Json;
using System.IO;
using ChatGPTCaller.Models;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;

namespace ChatGPTCaller.Services
{
    static public class ConversationService
    {
        static List<ChatGPT_API_Response.Message> messages = new List<ChatGPT_API_Response.Message>();
        static public List<ChatGPT_API_Response.Message> GetConversation(PromptRequest request)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            if (File.Exists(fileName))
            {
                FileStream f = File.Open(fileName, FileMode.Open);
                using(StreamReader sr = new StreamReader(f))
                {
                    string jsonContent = sr.ReadToEnd();
                    messages = JsonConvert.DeserializeObject<List<ChatGPT_API_Response.Message>>(jsonContent);
                    messages.Add(request.message);
                }
                return messages;
            }
            else
            {
                messages.Add(request.message);
                return messages;
            }
        }

        static public void RecordConversation(PromptRequest request, ChatGPT_API_Response.APIResponse response)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            
            if(File.Exists(fileName))
            {
                using(StreamReader sr = new StreamReader(fileName))
                {
                    string jsonContent = sr.ReadToEnd();
                    messages = JsonConvert.DeserializeObject<List<ChatGPT_API_Response.Message>>(jsonContent);
                    messages.Add(request.message);
                    messages.Add(response.choices[0].message);
                }
                using(StreamWriter sw = new StreamWriter(fileName, false))
                {
                    string jsonContent = JsonConvert.SerializeObject(messages, Formatting.Indented);
                    sw.Write(jsonContent);
                }
            }
            else
            {
                FileStream f = File.Create(fileName);
                using (StreamWriter sw = new StreamWriter(f))
                {
                    messages.Add(request.message);
                    messages.Add(response.choices[0].message);
                    string jsonContent = JsonConvert.SerializeObject(messages, Formatting.Indented);
                    sw.Write(jsonContent);
                }
                f.Close();
            }
        }
    }
}
