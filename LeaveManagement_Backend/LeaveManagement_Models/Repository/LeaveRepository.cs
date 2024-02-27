using LeaveManagement_Models.DataContext;
using LeaveManagement_Models.IRepository;
using LeaveManagement_Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.Repository
{
    public class LeaveRepository : IleaveRepository
    {
        private readonly LeaveManagementDataContext _context;

        public LeaveRepository(LeaveManagementDataContext context)
        {
            _context = context;
        }

        public async Task<LeaveBalance> LeaveBalance(string userId, int id)
        {
            var data = await _context.LeaveBalances.FirstOrDefaultAsync(c => c.UserId == userId && c.LeaveTypeId == id);
            return data;
        }

        public async Task<IEnumerable<Leave>> LeaveByUserID(string userId)
        {
            var user = _context.Leaves.Include(c=>c.User).Include(c => c.LeaveType).Where(c=>c.UserId == userId).ToList();
            return user;
        }
    }
}
