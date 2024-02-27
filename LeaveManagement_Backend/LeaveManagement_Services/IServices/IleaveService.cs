using LeaveManagement_Models.DTO;
using LeaveManagement_Models.Models;

namespace LeaveManagement_Services.IServices
{
    public interface IleaveService
    {
        Task<Leave> AddLeave(LeaveDTO leave);
        Task<IEnumerable<Leave>> GetAllLeaves();
        Task<IEnumerable<Leave>> GetLeaveUserId(string id);
        Task<bool> UpdateLeave(LeaveDTO leave);
    }
}
