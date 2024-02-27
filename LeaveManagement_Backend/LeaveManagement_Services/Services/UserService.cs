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
        private readonly IgenericRepository<ApplicationUser> _user;

        public UserService(IUserRepository userRepository, RoleManager<IdentityRole> roleManager, IgenericRepository<ApplicationUser> user)
        {
            _userRepository = userRepository;
            _roleManager = roleManager;
            _user = user;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
        {
            return await _user.GetAll();
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userRepository.GetById(id);
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
