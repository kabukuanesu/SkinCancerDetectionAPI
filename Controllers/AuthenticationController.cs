using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace SkinCancerDetectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager,
            AppDbContext context,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (model?.Username == null || model?.Password == null)
            {
                return BadRequest("Username and password are required");
            }

            // First check our custom authentication table
            var authRecord = _context.Authentications
                .FirstOrDefault(a => a.Username == model.Username);

            if (authRecord == null || !BCrypt.Net.BCrypt.Verify(model.Password, authRecord.Password))
            {
                return Unauthorized("Invalid username or password");
            }

            // Get the user from Identity
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized("User not found in identity system");
            }

            // Get the user details from our custom table
            var appUser = _context.ApplicationUsers
                .FirstOrDefault(u => u.UserId == authRecord.UserId);
            if (appUser == null)
            {
                return Unauthorized("User details not found");
            }

            // Get roles from Identity
            var roles = await _userManager.GetRolesAsync(user);

            // Generate JWT token
            var token = GenerateJwtToken(user, roles);

            return Ok(new
            {
                Token = token,
                User = new
                {
                    appUser.UserId,
                    appUser.FirstName,
                    appUser.LastName,
                    appUser.EmailAddress,
                    appUser.Role,
                    Roles = roles
                }
            });
        }

        [Authorize]
        [HttpPost("update")]
        public async Task<IActionResult> UpdateAuthentication([FromBody] UpdateAuthRequest model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            // Get current user ID from claims
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId == null)
            {
                return Unauthorized("Invalid user");
            }

            var identityUser = await _userManager.FindByIdAsync(currentUserId);
            if (identityUser == null)
            {
                return Unauthorized("Invalid user");
            }

            // Get the auth record
            var authRecord = _context.Authentications
                .FirstOrDefault(a => a.Username == identityUser.UserName);

            if (authRecord == null)
            {
                return NotFound("Authentication record not found");
            }

            // Verify current password
            if (!await _userManager.CheckPasswordAsync(identityUser, model.CurrentPassword))
            {
                return BadRequest("Current password is incorrect");
            }

            // Update password in Identity
            var changeResult = await _userManager.ChangePasswordAsync(
                identityUser,
                model.CurrentPassword,
                model.NewPassword);

            if (!changeResult.Succeeded)
            {
                return BadRequest(changeResult.Errors);
            }

            // Update our custom authentication record
            authRecord.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
            authRecord.RecoveryQuestion = model.RecoveryQuestion;
            authRecord.RecoveryAnswer = model.RecoveryAnswer;

            _context.Authentications.Update(authRecord);
            await _context.SaveChangesAsync();

            return Ok("Authentication updated successfully");
        }

        private string GenerateJwtToken(IdentityUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            // Add roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key is not configured")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(
                _configuration["Jwt:ExpireDays"] ?? "7"));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class UpdateAuthRequest
    {
        public string CurrentPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string RecoveryQuestion { get; set; } = string.Empty;
        public string RecoveryAnswer { get; set; } = string.Empty;
    }
}
