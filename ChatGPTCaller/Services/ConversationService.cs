using Newtonsoft.Json;
using System.IO;
using ChatGPTCaller.Models;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Collections;

namespace ChatGPTCaller.Services
{
    static public class ConversationService
    {
        static ArrayList messages = new ArrayList();
        static public ArrayList GetConversation(PromptRequest request)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            if (File.Exists(fileName))
            {
                FileStream f = File.Open(fileName, FileMode.Open);
                using(StreamReader sr = new StreamReader(f))
                {
                    string jsonContent = sr.ReadToEnd();
                    messages = JsonConvert.DeserializeObject<ArrayList>(jsonContent);
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
                    messages = JsonConvert.DeserializeObject<ArrayList>(jsonContent);
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
