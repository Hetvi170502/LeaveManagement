using LeaveManagement_Models.DTO;
using LeaveManagement_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Services.IServices
{
    public interface IleaveBalanceService
    {
        Task AddItemAsync(string UserId);
    }
}
