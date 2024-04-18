using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatGPTCaller.Models
{
    public class ThongKeDangNhap
    {
        public int Id { get; set; } 
        public DateTime LoginTime { get; set; }
        public DateTime? LogoutTime { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
