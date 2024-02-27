using LeaveManagement_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.DTO
{
    public class ApplicationUserDTO
    {
        public string Id {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string PhoneNumber {  get; set; }
        public string Password { get; set; }
        public string RoleNames { get; set; } // List of role names
    }
}
