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
    public class LeaveDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int LeaveTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime DateOfRequest { get; set; }

        public string ReasonForLeave { get; set; }

        public string Status { get; set; }
    }
}
