using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using SkinCancerDetectionAPI.Dtos.ManageWorkflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkinCancerDetectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ManageWorkflowController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ManageWorkflowController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Workflow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Workflow>>> GetWorkflows()
        {
            return await _context.Workflows
                .Include(w => w.Patient)
                .ToListAsync();
        }

        // GET: api/Workflow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Workflow>> GetWorkflow(int id)
        {
            var workflow = await _context.Workflows
                .Include(w => w.Patient)
                .FirstOrDefaultAsync(w => w.WorkflowId == id);

            if (workflow == null)
            {
                return NotFound();
            }

            return workflow;
        }

        // POST: api/Workflow
        [HttpPost]
        public async Task<ActionResult<Workflow>> PostWorkflow([FromBody] ManageWorkflowCreateDto dto)
        {
            // Validate the patient exists
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null)
            {
                return BadRequest("Invalid PatientId");
            }

            var workflow = new Workflow
            {
                PatientId = dto.PatientId,
                Status = "New",
                CreatedBy = User.Identity?.Name ?? "System",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.Workflows.Add(workflow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkflow", new { id = workflow.WorkflowId }, workflow);
        }

        // POST: api/Workflow/patient
        [HttpPost("patient")]
        public async Task<ActionResult<Workflow>> CreateWorkflowForPatient([FromBody] WorkflowPatientDto dto)
        {
            // Validate the patient exists
            var patient = await _context.Patients.FindAsync(dto.PatientId);
            if (patient == null)
            {
                return BadRequest("Invalid PatientId");
            }

            var workflow = new Workflow
            {
                PatientId = dto.PatientId,
                Status = "New",
                CreatedBy = User.Identity?.Name ?? "System",
                DateCreated = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            _context.Workflows.Add(workflow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWorkflow", new { id = workflow.WorkflowId }, workflow);
        }

        // PUT: api/Workflow/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorkflow(int id, Workflow workflow)
        {
            if (id != workflow.WorkflowId)
            {
                return BadRequest();
            }

            var existingWorkflow = await _context.Workflows.FindAsync(id);
            if (existingWorkflow == null)
            {
                return NotFound();
            }

            // Validate the patient exists if it's being changed
            if (existingWorkflow.PatientId != workflow.PatientId)
            {
                var patient = await _context.Patients.FindAsync(workflow.PatientId);
                if (patient == null)
                {
                    return BadRequest("Invalid PatientId");
                }
            }

            existingWorkflow.PatientId = workflow.PatientId;
            existingWorkflow.Status = workflow.Status;
            existingWorkflow.DateUpdated = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkflowExists(id))
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

        // PATCH: api/Workflow/5/status
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateWorkflowStatus(int id, [FromBody] string status)
        {
            var workflow = await _context.Workflows.FindAsync(id);
            if (workflow == null)
            {
                return NotFound();
            }

            workflow.Status = status;
            workflow.DateUpdated = DateTime.Now;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkflowExists(id))
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

        // DELETE: api/Workflow/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkflow(int id)
        {
            var workflow = await _context.Workflows.FindAsync(id);
            if (workflow == null)
            {
                return NotFound();
            }

            _context.Workflows.Remove(workflow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WorkflowExists(int id)
        {
            return _context.Workflows.Any(e => e.WorkflowId == id);
        }
    }

    public class WorkflowPatientDto
    {
        public int PatientId { get; set; }
        public bool IsScheduled { get; set; }
        public DateTime? Scheduled { get; set; }
    }
}
