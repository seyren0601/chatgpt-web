using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatGPTCaller.Models
{
    public class ThongKeTruyVan
    {
        public int Id { get; set; } 
        public string TruyVanText { get; set; }
        public string TraLoiText { get; set; }
        public DateTime ThoiGian { get; set; }

        public UserInfo UserInfo { get; set; }
    }
}
