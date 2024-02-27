using LeaveManagement_Models.DTO;
using LeaveManagement_Models.IRepository;
using LeaveManagement_Models.Models;
using LeaveManagement_Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Services.Services
{
    public class LeaveBalanceService : IleaveBalanceService
    {
        private readonly IgenericRepository<LeaveBalance> _leaveBalance;

        public LeaveBalanceService(IgenericRepository<LeaveBalance> leaveBalance)
        {
            _leaveBalance = leaveBalance; 
        }

        public async Task AddItemAsync(string UserId)
        {
            var leaveBalances = new List<LeaveBalanceDTO>
            {
            new LeaveBalanceDTO { UserId = UserId, LeaveTypeId = 1, Balance = 18 },
            new LeaveBalanceDTO { UserId = UserId, LeaveTypeId = 2, Balance = 17 },
            new LeaveBalanceDTO { UserId = UserId, LeaveTypeId = 3, Balance = 15 }
            };

            // Save the default leave balance records to the database
            foreach (var leave in leaveBalances)
            {
                var data = new LeaveBalance
                {
                    UserId = leave.UserId,
                    LeaveTypeId = leave.LeaveTypeId,
                    Balance = leave.Balance,
                };
               await _leaveBalance.AddItem(data);               
            }
          
        }
    }
}
