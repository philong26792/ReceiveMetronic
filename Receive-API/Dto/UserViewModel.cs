using System;

namespace Receive_API.Dto
{
    public class UserViewModel
    {
        public string ID { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int? RoleID { get; set; }
        public string RoleName {get;set;}
        public string DepID { get; set; }
        public string Department_Name {get;set;}
        public string Update_By { get; set; }
        public DateTime? Update_Time { get; set; }
    }
}