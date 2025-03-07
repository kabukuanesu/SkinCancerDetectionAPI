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
    public class DiagnosisController : ControllerBase
    {
        private readonly DiagnosisContext _context;

        public DiagnosisController(DiagnosisContext context)
        {
            _context = context;
        }

        // GET: api/Diagnosis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Diagnosis>>> GetDiagnoses()
        {
            return await _context.Diagnoses.ToListAsync();
        }

        // GET: api/Diagnosis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Diagnosis>> GetDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(id);

            if (diagnosis == null)
            {
                return NotFound();
            }

            return diagnosis;
        }

        // PUT: api/Diagnosis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiagnosis(int id, Diagnosis diagnosis)
        {
            if (id != diagnosis.DiagnosisId)
            {
                return BadRequest();
            }

            _context.Entry(diagnosis).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiagnosisExists(id))
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

        // POST: api/Diagnosis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Diagnosis>> PostDiagnosis(Diagnosis diagnosis)
        {
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiagnosis", new { id = diagnosis.DiagnosisId }, diagnosis);
        }

        // DELETE: api/Diagnosis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiagnosis(int id)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            _context.Diagnoses.Remove(diagnosis);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiagnosisExists(int id)
        {
            return _context.Diagnoses.Any(e => e.DiagnosisId == id);
        }
    }
}
