using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ChatGPTCaller.Models
{
    public class SinhVien
    {
        /*
	        id int auto_increment,
            full_name char(50),
               mssv int,
            gender char(50),
            birthday date,
            faculty char(50),
            major char(50),
            nationality char(50),
            religion char(50),
            idcard int,
            dateofissue date,
            placeofissue char(50),
            myphone int,
            parentphone int,
	        email char(50) unique,
            address char(50),
            aboutstudent char(255),
            hashed_pw char(50),
            salt char(50),*/
        [Key]
        public int id { get; set; }
        [DisplayName("Full-Name")]
        [Required]
        public string full_name { get; set; }
        [Required]
        public string mssv { get; set; }
        public string gender { get; set; } 
        public DateTimeFormat birthday { get; set; }
        public string faculty { get; set; }
        public string major { get; set; }
        public string nationality { get; set; }
        public string religion { get; set; }
        public string idcard { get; set; }
        public DateTime dateofissue { get; set; }
        public string placeofissue { get; set; }
        public string myphone { get; set; }
        public string parentphone { get; set; }
        [DisplayName("E-Mail")]
        [Required]
        public string email { get; set; }   
        public string address { get; set; }
        public string aboutstudent { get; set; }
        public string hashed_pw { get; set; }
        public string salt { get; set; }
    }
}
