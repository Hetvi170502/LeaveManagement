using Azure.Core;
using LeaveManagement_Models.DTO;
using LeaveManagement_Models.Models;
using LeaveManagement_Services.IServices;
using LeaveManagement_Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace LeaveManagement_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUserDTO request)
        {
            var result = await _userService.RegisterAsync(request, request.Password, request.RoleNames);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userService.LoginAsync(model.Email, model.Password);

            if (user != null)
            {
                // Authentication successful, generate JWT token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("Gjy7e&fn=Xv8jTQe8E+8$n7FPSQuUD2t7x!D&kz3tDsAzayXcd"); // Replace with your secret key
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, user.Email) // You can include more claims here if needed
                    }),
                    Expires = DateTime.UtcNow.AddDays(1), // Token expiration time
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                // Return token along with the login result
                return Ok(new { Token = tokenString, User = user });
            }

            // Authentication failed
            return Unauthorized();
        }
    }
}
