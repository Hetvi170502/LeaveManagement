using LeaveManagement_Models.DTO;
using LeaveManagement_Models.IRepository;
using LeaveManagement_Models.Models;
using LeaveManagement_Services.IServices;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUserRepository userRepository, RoleManager<IdentityRole> roleManager)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUserDTO> LoginAsync(string email, string password)
        {
            return await _userRepository.LoginAsync(email, password);
        }       

        public async Task<ApplicationUserDTO> RegisterAsync(ApplicationUserDTO obUser, string password, string role)
        {
            var user = new ApplicationUser
            {
                FirstName = obUser.FirstName,
                LastName = obUser.LastName,
                Email = obUser.Email,
                PhoneNumber = obUser.PhoneNumber,
                Department = obUser.Department,
                Designation = obUser.Designation
            };
            return await _userRepository.RegisterAsync(user, password, role);
        }
    }
}
