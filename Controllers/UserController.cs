using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateModel model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            var existingUser = await _context.ApplicationUsers.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update basic user info
            existingUser.FirstName = model.FirstName ?? existingUser.FirstName;
            existingUser.LastName = model.LastName ?? existingUser.LastName;

            if (model.DateOfBirth != null && DateOnly.TryParse(model.DateOfBirth, out var dateOfBirth))
            {
                existingUser.DateOfBirth = dateOfBirth;
            }

            existingUser.Gender = model.Gender ?? existingUser.Gender;
            existingUser.PhysicalAddress = model.PhysicalAddress ?? existingUser.PhysicalAddress;
            existingUser.PhoneNumber = model.PhoneNumber ?? existingUser.PhoneNumber;
            existingUser.Role = model.Role ?? existingUser.Role;
            existingUser.DateUpdated = DateTime.UtcNow;

            // Update authentication info if email/password is being changed
            if (!string.IsNullOrEmpty(model.Email) || !string.IsNullOrEmpty(model.Password))
            {
                var identityUser = await _userManager.FindByEmailAsync(existingUser.EmailAddress);
                if (identityUser == null)
                {
                    return BadRequest("Associated identity user not found");
                }

                if (!string.IsNullOrEmpty(model.Email))
                {
                    identityUser.Email = model.Email;
                    identityUser.UserName = model.Email;
                    existingUser.EmailAddress = model.Email;
                }

                if (!string.IsNullOrEmpty(model.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                    var result = await _userManager.ResetPasswordAsync(identityUser, token, model.Password);
                    if (!result.Succeeded)
                    {
                        return BadRequest(result.Errors);
                    }

                    // Update hashed password in our Authentication table
                    var auth = await _context.Authentications.FirstOrDefaultAsync(a => a.UserId == id);
                    if (auth != null)
                    {
                        auth.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    }
                }

                await _userManager.UpdateAsync(identityUser);
            }

            await _context.SaveChangesAsync();
            return Ok(new { Message = "User updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.ApplicationUsers.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Find and delete associated IdentityUser
            var identityUser = await _userManager.FindByEmailAsync(user.EmailAddress);
            if (identityUser != null)
            {
                await _userManager.DeleteAsync(identityUser);
            }

            // Delete authentication record
            var auth = await _context.Authentications.FirstOrDefaultAsync(a => a.UserId == id);
            if (auth != null)
            {
                _context.Authentications.Remove(auth);
            }

            // Delete user record
            _context.ApplicationUsers.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User deleted successfully" });
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

    public class UserUpdateModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Role { get; set; }
        public string? Password { get; set; }
        public string? RecoveryQuestion { get; set; }
        public string? RecoveryAnswer { get; set; }
    }
}
