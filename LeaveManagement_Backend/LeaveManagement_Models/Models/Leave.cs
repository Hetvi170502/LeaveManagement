using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.Models
{
    public class Leave : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("ApplicationUser")]
        public string UserId { get;set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("LeaveType")]
        public int LeaveTypeId {  get; set; }
        public LeaveType LeaveType { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime DateOfRequest { get; set; }

        [Required]
        public string ReasonForLeave { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
