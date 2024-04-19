using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatGPTCaller.Models
{
    public class ThongKeDangNhap
    {
        public int Id { get; set; }
        public string LoginTime { get; set; }
        public string? LogoutTime { get; set; }
    }
}
