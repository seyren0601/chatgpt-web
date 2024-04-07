using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ChatGPTCaller.Models
{
    public class Sinhvien
    {
        public string full_name { get; set; }
        public string mssv { get; set; }
        public string gender { get; set; } 
        public DateTime birthday { get; set; }
        public string faculty { get; set; }
        public string major { get; set; }
        public string nationality { get; set; }
        public string religion { get; set; }
        public string idcard { get; set; }
        public DateTime dateofissue { get; set; }
        public string placeofissue { get; set; }
        public string myphone { get; set; }
        public string parentphone { get; set; }
        public string email { get; set; }   
        public string address { get; set; }
        public string aboutstudent { get; set; }

    }
}
