using System;
using System.ComponentModel.DataAnnotations;

namespace Receive_API.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string DepID { get; set; }
        public string Status { get; set; }
        public string Updated_By { get; set; }

        public DateTime? Update_Time { get; set; }
    }
}