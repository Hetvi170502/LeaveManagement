using LeaveManagement_Models.DTO;
using LeaveManagement_Models.IRepository;
using LeaveManagement_Models.Models;
using LeaveManagement_Services.IServices;

namespace LeaveManagement_Services.Services
{
    public class LeaveService : IleaveService
    {
        private readonly IgenericRepository<Leave> _leave;
        private readonly IgenericRepository<LeaveBalance> _leaveBalance;

        private readonly IleaveRepository _leaveRepository;

        public LeaveService(IgenericRepository<Leave> leave , IleaveRepository leaveRepository, IgenericRepository<LeaveBalance> leaveBalance)
        {
            _leave = leave;
            _leaveRepository = leaveRepository;
            _leaveBalance = leaveBalance;
        }

        public async Task<Leave> AddLeave(LeaveDTO leave)
        {
            // Calculate the duration of the leave
            var leaveDuration = (leave.EndDate - leave.StartDate).Days + 1;

            // Retrieve the leave type balance
            var leaveType = await _leaveRepository.LeaveBalance(leave.UserId, leave.LeaveTypeId);
            if (leaveType != null)
            {
                // Check if the requested leave duration is less than or equal to the leave type balance
                if (leaveDuration <= leaveType.Balance)
                {
                    var data = new Leave
                    {
                        UserId = leave.UserId,
                        LeaveTypeId = leave.LeaveTypeId,
                        StartDate = leave.StartDate,
                        EndDate = leave.EndDate,
                        ReasonForLeave = leave.ReasonForLeave,
                        DateOfRequest = DateTime.Now,
                        Status = "InProgress",
                        CreatedOn = DateTime.Now,
                        IsEnabled = true
                    };

                    await _leave.AddItem(data);
                    return data;
                }
                else
                {
                    throw new Exception("Leave balance is not sufficient");
                }
            }
            else
            {
                throw new Exception("Leave type not found.");
            }
        }
       
        public async Task<IEnumerable<Leave>> GetAllLeaves()
        {
            return await _leave.GetAll( u => u.User, u => u.LeaveType);
        }

        public async Task<IEnumerable<Leave>> GetLeaveUserId(string id)
        {
            return await _leaveRepository.LeaveByUserID(id);
        }

        public async Task<bool> UpdateLeave(LeaveDTO leave)
        {
            var empId = await _leave.GetOneId(leave.Id);
            if(empId != null)
            {
                //var originalStatus = empId.Status;                
                var daysUntilLeaveStart = (empId.StartDate.Date - DateTime.Today).Days;
                if (leave.Status == "Approved")
                {
                    var leaveDuration = (empId.EndDate- empId.StartDate).Days + 1;
                    var leaveType = await _leaveRepository.LeaveBalance(empId.UserId,empId.LeaveTypeId);
                    if( leaveDuration != null)
                    {
                        var updateBalance = leaveType.Balance - leaveDuration;
                        if(updateBalance > 0)
                        {
                            leaveType.Balance = updateBalance;
                            await _leaveBalance.UpdateItem(leaveType);
                           
                        }
                        else
                        {
                            // Insufficient leave balance
                            throw new Exception("Insufficient leave balance.");
                        }
                    }
                    else
                    {
                        // Leave type not found
                        throw new Exception("Leave type not found.");
                    }
                }
                else if (leave.Status.ToLower() == "Cancel" && empId.Status.ToLower() == "Approved")
                {
                    if (daysUntilLeaveStart <= 3)
                    {
                        throw new Exception("You can cancel the leave before 3 days from your day of leave.");
                    }
                    else
                    {
                        var leaveDuration = (empId.EndDate - empId.StartDate).Days + 1;
                        var leaveType = await _leaveRepository.LeaveBalance(empId.UserId, empId.LeaveTypeId);
                        if (leaveType != null)
                        {
                            leaveType.Balance += leaveDuration;
                            await _leaveBalance.UpdateItem(leaveType); // Update leave balance
                            return true;
                        }
                    }
                   
                }
               
                else if (leave.Status.ToLower() == "Cancel")
                {
                    // Return true indicating leave was updated successfully
                    if (daysUntilLeaveStart <= 3)
                    {
                        throw new Exception("You can cancel the leave before 3 days from your day of leave.");
                    }
                    return true;
                }
               

                empId.Status = leave.Status;
                await _leave.UpdateItem(empId);
                return true;

            }
           
            throw new Exception("Leave not found.");

        }

    }

   }

