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
    public class ModelPredictionController : ControllerBase
    {
        private readonly ModelPredictionContext _context;

        public ModelPredictionController(ModelPredictionContext context)
        {
            _context = context;
        }

        // GET: api/ModelPrediction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelPrediction>>> GetModelPredictions()
        {
            return await _context.ModelPredictions.ToListAsync();
        }

        // GET: api/ModelPrediction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ModelPrediction>> GetModelPrediction(int id)
        {
            var modelPrediction = await _context.ModelPredictions.FindAsync(id);

            if (modelPrediction == null)
            {
                return NotFound();
            }

            return modelPrediction;
        }

        // PUT: api/ModelPrediction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModelPrediction(int id, ModelPrediction modelPrediction)
        {
            if (id != modelPrediction.ModelPredictionId)
            {
                return BadRequest();
            }

            _context.Entry(modelPrediction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelPredictionExists(id))
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

        // POST: api/ModelPrediction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ModelPrediction>> PostModelPrediction(ModelPrediction modelPrediction)
        {
            _context.ModelPredictions.Add(modelPrediction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetModelPrediction", new { id = modelPrediction.ModelPredictionId }, modelPrediction);
        }

        // DELETE: api/ModelPrediction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModelPrediction(int id)
        {
            var modelPrediction = await _context.ModelPredictions.FindAsync(id);
            if (modelPrediction == null)
            {
                return NotFound();
            }

            _context.ModelPredictions.Remove(modelPrediction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ModelPredictionExists(int id)
        {
            return _context.ModelPredictions.Any(e => e.ModelPredictionId == id);
        }
    }
}
