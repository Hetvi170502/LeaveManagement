using LeaveManagement_Models.DTO;
using LeaveManagement_Models.Models;
using LeaveManagement_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LeaveManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly IleaveService _leaveService;

        public LeaveController(IleaveService leaveService)
        {
            _leaveService = leaveService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLeave()
        {
            var data = await _leaveService.GetAllLeaves();
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> AddLeaves(LeaveDTO leaveDTO)
        {
            try
            {
                if (leaveDTO == null)
                {
                    return BadRequest();
                }

                var addLeave = await _leaveService.AddLeave(leaveDTO);
                return Ok(addLeave);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{empLeave}")]
        public async Task<IActionResult> GetLeave(string empLeave)
        {
            var Employe = await _leaveService.GetLeaveUserId(empLeave);
            return Ok(Employe);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLeaveStatus(LeaveDTO leaveDTO)
        {
            try
            {
                var updateResult = await _leaveService.UpdateLeave(leaveDTO);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CSV")]
        public async Task<IActionResult> DownloadAllLeave()
        {
            var data = await _leaveService.GetAllLeaves();

            // Convert data to CSV format
            var csvData = ToCsv(data);

            // Prepare response
            var fileName = "all_leaves.csv"; // or any other suitable filename
            var contentType = "text/csv";

            // Return file as a downloadable attachment
            return File(new System.Text.UTF8Encoding().GetBytes(csvData), contentType, fileName);
        }

        private string ToCsv(IEnumerable<Leave> leaves)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Employee Name,Start Date,End Date,Reason,Leave Type,Date Of Request"); // Add headers

            foreach (var leave in leaves)
            {
                var startDate = leave.StartDate.ToString("yyyy-MM-dd");
                var endDate = leave.EndDate.ToString("yyyy-MM-dd");
                var employeeName = $"{leave.User.FirstName} {leave.User.LastName}";
                sb.AppendLine($"{employeeName},{startDate},{endDate},{leave.ReasonForLeave},{leave.LeaveType.Type},{leave.DateOfRequest}");
            }

            return sb.ToString();
        }
    }
}
