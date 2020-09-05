using System;
using System.ComponentModel.DataAnnotations;

namespace Receive_API.Models
{
    public class Receive
    {
        [Key]
        public string ID { get; set; }
        public string UserID { get; set; }
        public string Accept_ID { get; set; }
        public string DepID {get;set;}
        public string ProductID { get; set; }
        public int Qty {get;set;}
        public DateTime? Register_Date { get; set; }
        public DateTime? Accept_Date { get; set; }
        public string Status { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Updated_Time { get; set; }
    }
}