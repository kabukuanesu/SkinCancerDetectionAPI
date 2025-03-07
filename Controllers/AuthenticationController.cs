using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Models;

namespace SkinCancerDetectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly AuthenticationContext _context;

        public AuthenticationController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/Authentication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Authentication>>> GetAuthentications()
        {
            return await _context.Authentications.ToListAsync();
        }

        // GET: api/Authentication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Authentication>> GetAuthentication(int id)
        {
            var authentication = await _context.Authentications.FindAsync(id);

            if (authentication == null)
            {
                return NotFound();
            }

            return authentication;
        }

        // PUT: api/Authentication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthentication(int id, Authentication authentication)
        {
            if (id != authentication.AuthenticationId)
            {
                return BadRequest();
            }

            _context.Entry(authentication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthenticationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authentication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Authentication>> PostAuthentication(Authentication authentication)
        {
            _context.Authentications.Add(authentication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthentication", new { id = authentication.AuthenticationId }, authentication);
        }

        // DELETE: api/Authentication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthentication(int id)
        {
            var authentication = await _context.Authentications.FindAsync(id);
            if (authentication == null)
            {
                return NotFound();
            }

            _context.Authentications.Remove(authentication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthenticationExists(int id)
        {
            return _context.Authentications.Any(e => e.AuthenticationId == id);
        }
    }
}
