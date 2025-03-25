using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using BCrypt.Net;

namespace SkinCancerDetectionAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;

        public UserController(
            UserManager<IdentityUser> userManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _context.ApplicationUsers.ToList();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] UserRegistrationModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            if (!DateOnly.TryParse(model.DateOfBirth, out var dateOfBirth))
            {
                return BadRequest("Invalid date format for DateOfBirth");
            }

            // Create IdentityUser
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Add to role
            await _userManager.AddToRoleAsync(user, model.Role);

            // Create User record
            var newUser = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = dateOfBirth,
                Gender = model.Gender,
                PhysicalAddress = model.PhysicalAddress,
                EmailAddress = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = model.Role,
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _context.ApplicationUsers.Add(newUser);
            await _context.SaveChangesAsync();

            var auth = new Authentication
            {
                UserId = newUser.UserId,
                Username = model.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password),
                RecoveryQuestion = model.RecoveryQuestion,
                RecoveryAnswer = model.RecoveryAnswer
            };

            _context.Authentications.Add(auth);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }
    }

    public class UserRegistrationModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string PhysicalAddress { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string RecoveryQuestion { get; set; } = string.Empty;
        public string RecoveryAnswer { get; set; } = string.Empty;
    }
}
