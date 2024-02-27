using LeaveManagement_Models.DTO;
using LeaveManagement_Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Services.IServices
{
    public interface IUserService
    {
        Task<ApplicationUserDTO> RegisterAsync(ApplicationUserDTO obUser, string password, string role);
        Task<ApplicationUserDTO> LoginAsync(string email, string password);
    }
}
