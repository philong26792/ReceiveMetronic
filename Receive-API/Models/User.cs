using System;
using System.ComponentModel.DataAnnotations;

namespace Receive_API.Models
{
    public class User
    {
        [Key]
        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? RoleID { get; set; }
        public string DepID { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Time { get; set; }
    }
}