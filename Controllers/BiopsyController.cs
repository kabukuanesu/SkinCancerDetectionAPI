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
    public class BiopsyController : ControllerBase
    {
        private readonly BiopsyContext _context;

        public BiopsyController(BiopsyContext context)
        {
            _context = context;
        }

        // GET: api/Biopsy
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Biopsy>>> GetBiopsies()
        {
            return await _context.Biopsies.ToListAsync();
        }

        // GET: api/Biopsy/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Biopsy>> GetBiopsy(int id)
        {
            var biopsy = await _context.Biopsies.FindAsync(id);

            if (biopsy == null)
            {
                return NotFound();
            }

            return biopsy;
        }

        // PUT: api/Biopsy/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBiopsy(int id, Biopsy biopsy)
        {
            if (id != biopsy.BiopsyId)
            {
                return BadRequest();
            }

            _context.Entry(biopsy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BiopsyExists(id))
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

        // POST: api/Biopsy
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Biopsy>> PostBiopsy(Biopsy biopsy)
        {
            _context.Biopsies.Add(biopsy);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBiopsy", new { id = biopsy.BiopsyId }, biopsy);
        }

        // DELETE: api/Biopsy/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBiopsy(int id)
        {
            var biopsy = await _context.Biopsies.FindAsync(id);
            if (biopsy == null)
            {
                return NotFound();
            }

            _context.Biopsies.Remove(biopsy);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BiopsyExists(int id)
        {
            return _context.Biopsies.Any(e => e.BiopsyId == id);
        }
    }
}
