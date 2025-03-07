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
    public class DermatologistController : ControllerBase
    {
        private readonly DermatologistContext _context;

        public DermatologistController(DermatologistContext context)
        {
            _context = context;
        }

        // GET: api/Dermatologist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dermatologist>>> GetDermatologists()
        {
            return await _context.Dermatologists.ToListAsync();
        }

        // GET: api/Dermatologist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dermatologist>> GetDermatologist(int id)
        {
            var dermatologist = await _context.Dermatologists.FindAsync(id);

            if (dermatologist == null)
            {
                return NotFound();
            }

            return dermatologist;
        }

        // PUT: api/Dermatologist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDermatologist(int id, Dermatologist dermatologist)
        {
            if (id != dermatologist.DermatologistId)
            {
                return BadRequest();
            }

            _context.Entry(dermatologist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DermatologistExists(id))
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

        // POST: api/Dermatologist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dermatologist>> PostDermatologist(Dermatologist dermatologist)
        {
            _context.Dermatologists.Add(dermatologist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDermatologist", new { id = dermatologist.DermatologistId }, dermatologist);
        }

        // DELETE: api/Dermatologist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDermatologist(int id)
        {
            var dermatologist = await _context.Dermatologists.FindAsync(id);
            if (dermatologist == null)
            {
                return NotFound();
            }

            _context.Dermatologists.Remove(dermatologist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DermatologistExists(int id)
        {
            return _context.Dermatologists.Any(e => e.DermatologistId == id);
        }
    }
}
