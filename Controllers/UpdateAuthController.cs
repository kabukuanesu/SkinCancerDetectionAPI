using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using BCrypt.Net;
using System.ComponentModel.DataAnnotations;

namespace SkinCancerDetectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateAuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UpdateAuthController(AppDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPut]
        public IActionResult UpdateAuthentication([FromBody] AuthUpdateRequest model)
        {
            if (model == null)
            {
                return BadRequest("Invalid request");
            }

            try
            {
                // Find the authentication record
                var authRecord = _context.Authentications
                    .FirstOrDefault(a => a.Username == model.Username);

                if (authRecord == null)
                {
                    return NotFound("User not found");
                }

                // Verify current password
                if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, authRecord.Password))
                {
                    return BadRequest("Current password is incorrect");
                }

                // Update the fields
                authRecord.Password = BCrypt.Net.BCrypt.HashPassword(model.NewPassword);
                authRecord.RecoveryQuestion = model.RecoveryQuestion;
                authRecord.RecoveryAnswer = model.RecoveryAnswer;

                _context.Authentications.Update(authRecord);
                _context.SaveChanges();

                return Ok(new
                {
                    Message = "Authentication details updated successfully",
                    Username = model.Username,
                    UpdatedAt = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                // Log the error
                _context.Logs.Add(new Log
                {
                    Message = $"Update failed: {ex.Message}",
                    StackTrace = ex.StackTrace ?? string.Empty,
                    Timestamp = DateTime.UtcNow
                });
                _context.SaveChanges();

                return StatusCode(500, "An error occurred while updating authentication");
            }
        }
    }

    public class AuthUpdateRequest
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string CurrentPassword { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; } = string.Empty;

        [Required]
        public string RecoveryQuestion { get; set; } = string.Empty;

        [Required]
        public string RecoveryAnswer { get; set; } = string.Empty;
    }
}
