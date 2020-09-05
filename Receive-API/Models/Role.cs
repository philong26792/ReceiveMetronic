using System;
using System.ComponentModel.DataAnnotations;

namespace Receive_API.Models
{
    public class Role
    {
        [Key]
        public int ID {get;set;}
        public string Name {get;set;}
        public string Status {get;set;}
        public string Update_By {get;set;}
        public DateTime? Update_Time {get;set;}
    }
}