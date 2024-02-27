using LeaveManagement_Models.DTO;
using LeaveManagement_Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

                return Ok("Leave status updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
