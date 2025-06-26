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
    public class LogsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LogsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Logs/user/5
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<object>> GetUserData(int userId)
        {
            var result = new
            {
                User = await _context.ApplicationUsers.FindAsync(userId),
                Authentications = await _context.Authentications
                    .Where(a => a.UserId == userId)
                    .ToListAsync(),
                Patients = await _context.Patients
                    .Where(p => p.UserId == userId)
                    .ToListAsync(),
                Bookings = await _context.Bookings
                    .Where(b => b.UserId == userId)
                    .ToListAsync(),
                Lesions = await _context.Lesions
                    .Where(l => l.UserId == userId)
                    .ToListAsync(),
                Workflows = await _context.Workflows
                    .Where(w => w.Patient.UserId == userId)
                    .ToListAsync(),
                Biopsies = await _context.Biopsies
                    .Where(b => b.UserId == userId)
                    .ToListAsync(),
                Images = await _context.Images
                    .Where(i => i.UserId == userId)
                    .ToListAsync(),
                WorkflowStages = await _context.WorkflowStages
                    .Where(ws => ws.Workflow.Patient.UserId == userId)
                    .ToListAsync(),
                Diagnoses = await _context.Diagnoses
                    .Where(d => d.UserId == userId)
                    .ToListAsync(),
                ModelPredictions = await _context.ModelPredictions
                    .Where(mp => mp.UserId == userId)
                    .ToListAsync(),
                FinalDiagnoses = await _context.FinalDiagnoses
                    .Where(fd => fd.UserId == userId)
                    .ToListAsync(),
                Treatments = await _context.Treatments
                    .Where(t => t.UserId == userId)
                    .ToListAsync(),
                FollowUps = await _context.FollowUps
                    .Where(fu => fu.UserId == userId)
                    .ToListAsync()
            };

            if (result.User == null)
            {
                return NotFound("User not found");
            }

            return Ok(result);
        }

        // GET: api/Logs/patient/5
        [HttpGet("patient/{patientId}")]
        public async Task<ActionResult<object>> GetPatientData(int patientId)
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
                Bookings = await _context.Bookings
                    .Where(b => b.PatientId == patientId)
                    .ToListAsync(),
                Lesions = await _context.Lesions
                    .Where(l => l.PatientId == patientId)
                    .ToListAsync(),
                Workflows = await _context.Workflows
                    .Where(w => w.PatientId == patientId)
                    .ToListAsync(),
                Biopsies = await _context.Biopsies
                    .Include(b => b.Lesion)
                    .Where(b => b.Lesion != null && b.Lesion.PatientId == patientId)
                    .ToListAsync(),
                Images = await _context.Images
                    .Include(i => i.Lesion)
                    .Where(i => i.Lesion != null && i.Lesion.PatientId == patientId)
                    .ToListAsync(),
                WorkflowStages = await _context.WorkflowStages
                    .Include(ws => ws.Workflow)
                    .Where(ws => ws.Workflow != null && ws.Workflow.PatientId == patientId)
                    .ToListAsync(),
                Diagnoses = await _context.Diagnoses
                    .Include(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Where(d => d.Biopsy != null && d.Biopsy.Lesion != null && d.Biopsy.Lesion.PatientId == patientId)
                    .ToListAsync(),
                ModelPredictions = await _context.ModelPredictions
                    .Include(mp => mp.Image)
                    .ThenInclude(i => i.Lesion)
                    .Where(mp => mp.Image != null && mp.Image.Lesion != null && mp.Image.Lesion.PatientId == patientId)
                    .ToListAsync(),
                FinalDiagnoses = await _context.FinalDiagnoses
                    .Include(fd => fd.Diagnosis)
                    .ThenInclude(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Include(fd => fd.ModelPrediction)
                    .ThenInclude(mp => mp.Image)
                    .ThenInclude(i => i.Lesion)
                    .Where(fd => (fd.Diagnosis != null &&
                                fd.Diagnosis.Biopsy != null &&
                                fd.Diagnosis.Biopsy.Lesion != null &&
                                fd.Diagnosis.Biopsy.Lesion.PatientId == patientId) ||
                               (fd.ModelPrediction != null &&
                                fd.ModelPrediction.Image != null &&
                                fd.ModelPrediction.Image.Lesion != null &&
                                fd.ModelPrediction.Image.Lesion.PatientId == patientId))
                    .ToListAsync(),
                Treatments = await _context.Treatments
                    .Include(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.Diagnosis)
                    .ThenInclude(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Include(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.ModelPrediction)
                    .ThenInclude(mp => mp.Image)
                    .ThenInclude(i => i.Lesion)
                    .Where(t => t.FinalDiagnosis != null &&
                              ((t.FinalDiagnosis.Diagnosis != null &&
                                t.FinalDiagnosis.Diagnosis.Biopsy != null &&
                                t.FinalDiagnosis.Diagnosis.Biopsy.Lesion != null &&
                                t.FinalDiagnosis.Diagnosis.Biopsy.Lesion.PatientId == patientId) ||
                               (t.FinalDiagnosis.ModelPrediction != null &&
                                t.FinalDiagnosis.ModelPrediction.Image != null &&
                                t.FinalDiagnosis.ModelPrediction.Image.Lesion != null &&
                                t.FinalDiagnosis.ModelPrediction.Image.Lesion.PatientId == patientId)))
                    .ToListAsync(),
                FollowUps = await _context.FollowUps
                    .Include(fu => fu.Treatment)
                    .ThenInclude(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.Diagnosis)
                    .ThenInclude(d => d.Biopsy)
                    .ThenInclude(b => b.Lesion)
                    .Include(fu => fu.Treatment)
                    .ThenInclude(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.ModelPrediction)
                    .ThenInclude(mp => mp.Image)
                    .ThenInclude(i => i.Lesion)
                    .Where(fu => fu.Treatment != null &&
                                fu.Treatment.FinalDiagnosis != null &&
                               ((fu.Treatment.FinalDiagnosis.Diagnosis != null &&
                                 fu.Treatment.FinalDiagnosis.Diagnosis.Biopsy != null &&
                                 fu.Treatment.FinalDiagnosis.Diagnosis.Biopsy.Lesion != null &&
                                 fu.Treatment.FinalDiagnosis.Diagnosis.Biopsy.Lesion.PatientId == patientId) ||
                                (fu.Treatment.FinalDiagnosis.ModelPrediction != null &&
                                 fu.Treatment.FinalDiagnosis.ModelPrediction.Image != null &&
                                 fu.Treatment.FinalDiagnosis.ModelPrediction.Image.Lesion != null &&
                                 fu.Treatment.FinalDiagnosis.ModelPrediction.Image.Lesion.PatientId == patientId)))
                    .ToListAsync()
            };

            return Ok(result);
        }
    }
}
