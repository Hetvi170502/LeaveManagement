using LeaveManagement_Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.DTO
{
    public class LeaveBalanceDTO
    {
        public int Id { get; set; }
      
        public string UserId { get; set; }
       
        public int LeaveTypeId { get; set; }
    
        public int Balance { get; set; }
    }
}
