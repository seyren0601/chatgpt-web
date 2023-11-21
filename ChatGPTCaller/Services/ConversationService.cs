using Newtonsoft.Json;
using System.IO;
using ChatGPTCaller.Models;
using System.Collections.Generic;

namespace ChatGPTCaller.Services
{
    static public class ConversationService
    {
        static public void GetConversation(PromptRequest request)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            if (!File.Exists(fileName))
            {
                File.Create(fileName);
            }
            else
            {
                using (StreamReader sr = new StreamReader(fileName))
                {
                    string jsonString = sr.ReadToEnd();
                    JsonSerializer jsonSerializer;
                }
            }
        }

        static public void RecordConversation(PromptRequest request, ChatGPT_API_Response.APIResponse response)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            List<ChatGPT_API_Response.Message> messages = new List<ChatGPT_API_Response.Message>();
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
