using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.Models
{
    public class LeaveType : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get;set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime ValidityFrom { get; set; }

        [Required]
        public DateTime ValidityTo { get; set; }
    }
}
