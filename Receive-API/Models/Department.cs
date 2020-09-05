using System;
using System.ComponentModel.DataAnnotations;

namespace Receive_API.Models
{
    public class Department
    {
        [Key]
        public string ID { get; set; }
        public string Name_ZW { get; set; }
        public string Name_LL { get; set; }
        public string Name_EN { get; set; }
        public string Status { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Updated_Time { get; set; }
    }
}