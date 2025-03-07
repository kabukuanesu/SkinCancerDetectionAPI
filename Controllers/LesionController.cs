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
    public class LesionController : ControllerBase
    {
        private readonly LesionContext _context;

        public LesionController(LesionContext context)
        {
            _context = context;
        }

        // GET: api/Lesion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Lesion>>> GetLesions()
        {
            return await _context.Lesions.ToListAsync();
        }

        // GET: api/Lesion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Lesion>> GetLesion(int id)
        {
            var lesion = await _context.Lesions.FindAsync(id);

            if (lesion == null)
            {
                return NotFound();
            }

            return lesion;
        }

        // PUT: api/Lesion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLesion(int id, Lesion lesion)
        {
            if (id != lesion.LesionId)
            {
                return BadRequest();
            }

            _context.Entry(lesion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LesionExists(id))
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

        // POST: api/Lesion
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Lesion>> PostLesion(Lesion lesion)
        {
            _context.Lesions.Add(lesion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLesion", new { id = lesion.LesionId }, lesion);
        }

        // DELETE: api/Lesion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLesion(int id)
        {
            var lesion = await _context.Lesions.FindAsync(id);
            if (lesion == null)
            {
                return NotFound();
            }

            _context.Lesions.Remove(lesion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LesionExists(int id)
        {
            return _context.Lesions.Any(e => e.LesionId == id);
        }
    }
}
