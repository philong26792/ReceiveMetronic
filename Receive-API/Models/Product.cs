using System;
using System.ComponentModel.DataAnnotations;

namespace Receive_API.Models
{
    public class Product
    {
        [Key]
        public string ID { get; set; }
        public string Name { get; set; }
        public int? CatID { get; set; }
        public string Status { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Update_Time { get; set; }
    }
}