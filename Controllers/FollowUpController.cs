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
    public class FollowUpController : ControllerBase
    {
        private readonly FollowUpContext _context;

        public FollowUpController(FollowUpContext context)
        {
            _context = context;
        }

        // GET: api/FollowUp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FollowUp>>> GetFollowUps()
        {
            return await _context.FollowUps.ToListAsync();
        }

        // GET: api/FollowUp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FollowUp>> GetFollowUp(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);

            if (followUp == null)
            {
                return NotFound();
            }

            return followUp;
        }

        // PUT: api/FollowUp/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowUp(int id, FollowUp followUp)
        {
            if (id != followUp.FollowUpId)
            {
                return BadRequest();
            }

            _context.Entry(followUp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowUpExists(id))
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

        // POST: api/FollowUp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FollowUp>> PostFollowUp(FollowUp followUp)
        {
            _context.FollowUps.Add(followUp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowUp", new { id = followUp.FollowUpId }, followUp);
        }

        // DELETE: api/FollowUp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowUp(int id)
        {
            var followUp = await _context.FollowUps.FindAsync(id);
            if (followUp == null)
            {
                return NotFound();
            }

            _context.FollowUps.Remove(followUp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowUpExists(int id)
        {
            return _context.FollowUps.Any(e => e.FollowUpId == id);
        }
    }
}
