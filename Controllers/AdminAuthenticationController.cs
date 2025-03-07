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
    public class AdminAuthenticationController : ControllerBase
    {
        private readonly AdminAuthenticationContext _context;

        public AdminAuthenticationController(AdminAuthenticationContext context)
        {
            _context = context;
        }

        // GET: api/AdminAuthentication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminAuthentication>>> GetAdminAuthentications()
        {
            return await _context.AdminAuthentications.ToListAsync();
        }

        // GET: api/AdminAuthentication/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminAuthentication>> GetAdminAuthentication(int id)
        {
            var adminAuthentication = await _context.AdminAuthentications.FindAsync(id);

            if (adminAuthentication == null)
            {
                return NotFound();
            }

            return adminAuthentication;
        }

        // PUT: api/AdminAuthentication/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminAuthentication(int id, AdminAuthentication adminAuthentication)
        {
            if (id != adminAuthentication.AdminAuthenticationId)
            {
                return BadRequest();
            }

            _context.Entry(adminAuthentication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminAuthenticationExists(id))
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

        // POST: api/AdminAuthentication
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdminAuthentication>> PostAdminAuthentication(AdminAuthentication adminAuthentication)
        {
            _context.AdminAuthentications.Add(adminAuthentication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminAuthentication", new { id = adminAuthentication.AdminAuthenticationId }, adminAuthentication);
        }

        // DELETE: api/AdminAuthentication/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminAuthentication(int id)
        {
            var adminAuthentication = await _context.AdminAuthentications.FindAsync(id);
            if (adminAuthentication == null)
            {
                return NotFound();
            }

            _context.AdminAuthentications.Remove(adminAuthentication);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminAuthenticationExists(int id)
        {
            return _context.AdminAuthentications.Any(e => e.AdminAuthenticationId == id);
        }
    }
}
