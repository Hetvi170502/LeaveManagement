using LeaveManagement_Models.DTO;
using LeaveManagement_Models.IRepository;
using LeaveManagement_Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagement_Models.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return null;
            else
                return user;
        }

        public async Task<ApplicationUserDTO> LoginAsync(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded)
            {

                var user = await _userManager.FindByEmailAsync(email);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);


                    var userDTO = new ApplicationUserDTO
                    {
                        Id = user.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = email,
                        Password = password,
                        Department = user.Department,
                        Designation = user.Designation,
                        PhoneNumber = user.PhoneNumber,
                        RoleNames = roles.FirstOrDefault()

                    };
                    return userDTO;
                }
            }
            return null;
        }

        public async Task<ApplicationUserDTO> RegisterAsync(ApplicationUser obUser, string password, string role)
        {
            var user = new ApplicationUser
            {
                UserName = obUser.Email,
                Email = obUser.Email,
                PhoneNumber = obUser.PhoneNumber,
                FirstName = obUser.FirstName,
                LastName = obUser.LastName,
                IsActive = true,
                CreatedOn = DateTime.Now,
                CreatedBy = obUser.CreatedBy,
                Department = obUser.Department,
                Designation = obUser.Designation,
            };
            var result = await _userManager.CreateAsync(user, password);
            if (result != null)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            var userDTO = new ApplicationUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                FirstName = user.FirstName,
                LastName = user.LastName,             
                Department = user.Department,
                Designation = user.Designation,
                RoleNames = role
            };

            return userDTO;
        }

        
    }
}
