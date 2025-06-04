using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using System.Security.Claims;

namespace SkinCancerDetectionAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserProfileController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<User>> GetUserProfile(int userId)
        {
            // Get the currently authenticated user's ID from claims
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // If you need to verify the requesting user has access to this profile
            // You could add authorization logic here

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }

        // GET: api/UserProfile/me
        [HttpGet("me")]
        public async Task<ActionResult<User>> GetCurrentUserProfile()
        {
            // Get the currently authenticated user's ID from claims
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(currentUserId))
            {
                return Unauthorized("User not authenticated");
            }

            // If your UserId is an int, you might need to parse it
            if (!int.TryParse(currentUserId, out int userId))
            {
                return BadRequest("Invalid user ID format");
            }

            var user = await _context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(user);
        }
    }
}
