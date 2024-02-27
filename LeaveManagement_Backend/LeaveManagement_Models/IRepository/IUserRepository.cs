using LeaveManagement_Models.DTO;
using LeaveManagement_Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.IRepository
{
    public interface IUserRepository
    {
        Task<ApplicationUserDTO> RegisterAsync(ApplicationUser obUser, string password, string role);
        Task<ApplicationUserDTO> LoginAsync(string email, string password);

        Task<ApplicationUser> GetById(string id);
    }
}
