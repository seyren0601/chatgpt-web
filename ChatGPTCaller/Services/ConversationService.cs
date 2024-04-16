using Newtonsoft.Json;
using System.IO;
using ChatGPTCaller.Models;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Collections;
using Microsoft.AspNetCore.Hosting;

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

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Conversations");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);  // Create the directory if it doesn't exist
            }
            string fileName = request.ConversationID.ToString() + ".json";
            var filePath = Path.Combine(uploadsFolder, fileName);


            if (File.Exists(filePath))
            {
                using(StreamReader sr = new StreamReader(filePath))
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
                using(StreamWriter sw = new StreamWriter(filePath, false))
                {
                    string jsonContent = JsonConvert.SerializeObject(thread, Formatting.Indented);
                    sw.Write(jsonContent);
                }
            }
            else
            {

                FileStream f = File.Create(filePath);
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
