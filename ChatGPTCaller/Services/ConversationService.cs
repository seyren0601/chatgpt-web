using Newtonsoft.Json;
using System.IO;
using ChatGPTCaller.Models;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Collections;

namespace ChatGPTCaller.Services
{
    public class ConversationService
    {
        ArrayList thread;
        public ConversationService() 
        {
            thread = new ArrayList();
        }
        public ArrayList GetConversation(PromptRequest request)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            if (File.Exists(fileName))
            {
                FileStream f = File.Open(fileName, FileMode.Open);
                using(StreamReader sr = new StreamReader(f))
                {
                    string jsonContent = sr.ReadToEnd();
                    thread = JsonConvert.DeserializeObject<ArrayList>(jsonContent);
                }
                f.Close();
            }
            return thread;
        }

        public void RecordConversation(PromptRequest request, ChatGPT_API_Response.APIResponse response)
        {
            string fileName = request.ConversationID.ToString() + ".json";
            
            if(File.Exists(fileName))
            {
                using(StreamReader sr = new StreamReader(fileName))
                {
                    string jsonContent = sr.ReadToEnd();
                    thread = JsonConvert.DeserializeObject<ArrayList>(jsonContent);
                    thread.Add(new
                    {
                        role = "user",
                        content = request.message.content
                    });
                    thread.Add(new
                    {
                        role = "assistant",
                        content = response.choices[0].message.content
                    });
                }
                using(StreamWriter sw = new StreamWriter(fileName, false))
                {
                    string jsonContent = JsonConvert.SerializeObject(thread, Formatting.Indented);
                    sw.Write(jsonContent);
                }
            }
            else
            {
                FileStream f = File.Create(fileName);
                using (StreamWriter sw = new StreamWriter(f))
                {
                    thread.Add(new
                    {
                        role = "user",
                        content = request.message.content
                    });
                    thread.Add(new
                    {
                        role = "assistant",
                        content = response.choices[0].message.content
                    });
                    string jsonContent = JsonConvert.SerializeObject(thread, Formatting.Indented);
                    sw.Write(jsonContent);
                }
                f.Close();
            }
        }
    }
}
