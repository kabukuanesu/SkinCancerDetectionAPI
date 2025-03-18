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
    public class FinalDiagnosisController : ControllerBase
    {
        private readonly FinalDiagnosisContext _context;

        public FinalDiagnosisController(FinalDiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/FinalDiagnosis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinalDiagnosis>>> GetFinalDiagnoses()
        {
            return await _context.FinalDiagnoses.ToListAsync();
        }

        // GET: api/FinalDiagnosis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FinalDiagnosis>> GetFinalDiagnosis(int id)
        {
            var finalDiagnosis = await _context.FinalDiagnoses.FindAsync(id);

            if (finalDiagnosis == null)
            {
                return NotFound();
            }

            return finalDiagnosis;
        }

        // PUT: api/FinalDiagnosis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFinalDiagnosis(int id, FinalDiagnosis finalDiagnosis)
        {
            if (id != finalDiagnosis.FinalDiagnosisId)
            {
                return BadRequest();
            }

            _context.Entry(finalDiagnosis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FinalDiagnosisExists(id))
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

        // POST: api/FinalDiagnosis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FinalDiagnosis>> PostFinalDiagnosis(FinalDiagnosis finalDiagnosis)
        {
            _context.FinalDiagnoses.Add(finalDiagnosis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFinalDiagnosis", new { id = finalDiagnosis.FinalDiagnosisId }, finalDiagnosis);
        }

        // DELETE: api/FinalDiagnosis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFinalDiagnosis(int id)
        {
            var finalDiagnosis = await _context.FinalDiagnoses.FindAsync(id);
            if (finalDiagnosis == null)
            {
                return NotFound();
            }

            _context.FinalDiagnoses.Remove(finalDiagnosis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FinalDiagnosisExists(int id)
        {
            return _context.FinalDiagnoses.Any(e => e.FinalDiagnosisId == id);
        }
    }
}
