using LeaveManagement_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.IRepository
{
    public interface IleaveRepository
    { 
        Task<IEnumerable<Leave>> LeaveByUserID(string userId);
        Task<LeaveBalance> LeaveBalance(string userId, int id);
     
    }
}
