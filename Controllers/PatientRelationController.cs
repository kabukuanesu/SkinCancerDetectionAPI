using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SkinCancerDetectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientRelationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PatientRelationController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/PatientRelation/5
        [HttpGet("{patientId}")]
        public async Task<ActionResult<string>> GetPatientRelationships(int patientId)
        {
            // First verify the patient exists
            var patientExists = await _context.Patients.AnyAsync(p => p.PatientId == patientId);
            if (!patientExists)
            {
                return NotFound("Patient not found");
            }

            // Check workflow relationships in order of the treatment pipeline
            if (await HasFollowUp(patientId))
            {
                return Ok("FollowUp");
            }
            if (await HasTreatment(patientId))
            {
                return Ok("Treatment");
            }
            if (await HasFinalDiagnosis(patientId))
            {
                return Ok("FinalDiagnosis");
            }
            if (await HasDiagnosis(patientId))
            {
                return Ok("Diagnosis");
            }
            if (await HasBiopsy(patientId))
            {
                return Ok("Biopsy");
            }
            if (await HasLesion(patientId))
            {
                return Ok("Lesion");
            }
            if (await HasWorkflow(patientId))
            {
                return Ok("Workflow");
            }

            return Ok("Patient (no further relationships)");
        }

        private async Task<bool> HasWorkflow(int patientId)
        {
            return await _context.Workflows
                .AnyAsync(w => w.PatientId == patientId);
        }

        private async Task<bool> HasLesion(int patientId)
        {
            return await _context.Lesions
                .AnyAsync(l => l.PatientId == patientId);
        }

        private async Task<bool> HasBiopsy(int patientId)
        {
            return await _context.Biopsies
                .Include(b => b.Lesion)
                .AnyAsync(b => b.Lesion.PatientId == patientId);
        }

        private async Task<bool> HasDiagnosis(int patientId)
        {
            return await _context.Diagnoses
                .Include(d => d.Biopsy)
                .ThenInclude(b => b.Lesion)
                .AnyAsync(d => d.Biopsy.Lesion.PatientId == patientId);
        }

        private async Task<bool> HasFinalDiagnosis(int patientId)
        {
            return await _context.FinalDiagnoses
                .Include(fd => fd.Diagnosis)
                .ThenInclude(d => d.Biopsy)
                .ThenInclude(b => b.Lesion)
                .AnyAsync(fd => fd.Diagnosis.Biopsy.Lesion.PatientId == patientId);
        }

        private async Task<bool> HasTreatment(int patientId)
        {
            return await _context.Treatments
                .Include(t => t.FinalDiagnosis)
                .ThenInclude(fd => fd.Diagnosis)
                .ThenInclude(d => d.Biopsy)
                .ThenInclude(b => b.Lesion)
                .AnyAsync(t => t.FinalDiagnosis.Diagnosis.Biopsy.Lesion.PatientId == patientId);
        }

        private async Task<bool> HasFollowUp(int patientId)
        {
            return await _context.FollowUps
                .Include(fu => fu.Treatment)
                .ThenInclude(t => t.FinalDiagnosis)
                .ThenInclude(fd => fd.Diagnosis)
                .ThenInclude(d => d.Biopsy)
                .ThenInclude(b => b.Lesion)
                .AnyAsync(fu => fu.Treatment.FinalDiagnosis.Diagnosis.Biopsy.Lesion.PatientId == patientId);
        }

        // GET: api/PatientRelation/5/details
        [HttpGet("{patientId}/details")]
        public async Task<ActionResult<object>> GetPatientRelationshipDetails(int patientId)
        {
            // First verify the patient exists
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
            {
                return NotFound("Patient not found");
            }

            var result = new
            {
                Patient = patient,
                Workflows = await _context.Workflows
                    .Where(w => w.PatientId == patientId)
                    .ToListAsync(),
                Lesions = await _context.Lesions
                    .Where(l => l.PatientId == patientId)
                    .ToListAsync(),
                Biopsies = await _context.Biopsies
                    .Include(b => b.Lesion)
                    .Where(b => b.Lesion.PatientId == patientId)
                    .ToListAsync(),
                Diagnoses = await _context.Diagnoses
                    .Include(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Where(d => d.Biopsy.Lesion.PatientId == patientId)
                    .ToListAsync(),
                FinalDiagnoses = await _context.FinalDiagnoses
                    .Include(fd => fd.Diagnosis)
                    .ThenInclude(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Where(fd => fd.Diagnosis.Biopsy.Lesion.PatientId == patientId)
                    .ToListAsync(),
                Treatments = await _context.Treatments
                    .Include(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.Diagnosis)
                    .ThenInclude(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Where(t => t.FinalDiagnosis.Diagnosis.Biopsy.Lesion.PatientId == patientId)
                    .ToListAsync(),
                FollowUps = await _context.FollowUps
                    .Include(fu => fu.Treatment)
                    .ThenInclude(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.Diagnosis)
                    .ThenInclude(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Where(fu => fu.Treatment.FinalDiagnosis.Diagnosis.Biopsy.Lesion.PatientId == patientId)
                    .ToListAsync()
            };

            return Ok(result);
        }
    }
}
