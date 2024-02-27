using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.DTO
{
    public class LeaveTypeDTO
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Description { get; set; }

        public DateTime ValidityFrom { get; set; }

        public DateTime ValidityTo { get; set; }
    }
}
