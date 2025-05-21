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
        public async Task<ActionResult<object>> GetPatientRelationships(int patientId)
        {
            // First verify the patient exists
            var patientExists = await _context.Patients.AnyAsync(p => p.PatientId == patientId);
            if (!patientExists)
            {
                return NotFound("Patient not found");
            }

            // Check workflow relationships in order of the treatment pipeline
            var followUp = await GetFollowUp(patientId);
            if (followUp != null)
            {
                return Ok(new { Table = "FollowUp", FollowUpId = followUp.FollowUpId });
            }

            var treatment = await GetTreatment(patientId);
            if (treatment != null)
            {
                return Ok(new { Table = "Treatment", TreatmentId = treatment.TreatmentId });
            }

            var finalDiagnosis = await GetFinalDiagnosis(patientId);
            if (finalDiagnosis != null)
            {
                return Ok(new { Table = "FinalDiagnosis", FinalDiagnosisId = finalDiagnosis.FinalDiagnosisId });
            }

            var modelPrediction = await GetModelPrediction(patientId);
            if (modelPrediction != null)
            {
                return Ok(new { Table = "ModelPrediction", ModelPredictionId = modelPrediction.ModelPredictionId });
            }

            var image = await GetImage(patientId);
            if (image != null)
            {
                return Ok(new { Table = "Image", ImageId = image.ImageId });
            }

            var lesion = await GetLesion(patientId);
            if (lesion != null)
            {
                return Ok(new { Table = "Lesion", LesionId = lesion.LesionId });
            }

            return Ok(new { Table = "Patient", PatientId = patientId });
        }

        private async Task<Lesion?> GetLesion(int patientId)
        {
            return await _context.Lesions
                .FirstOrDefaultAsync(l => l.PatientId == patientId);
        }

        private async Task<Image?> GetImage(int patientId)
        {
            return await _context.Images
                .Include(b => b.Lesion)
                .FirstOrDefaultAsync(b => b.Lesion != null && b.Lesion.PatientId == patientId);
        }

        private async Task<ModelPrediction?> GetModelPrediction(int patientId)
        {
            return await _context.ModelPredictions
                .Include(d => d.Image)
                .ThenInclude(b => b.Lesion)
                .FirstOrDefaultAsync(d => d.Image != null &&
                                          d.Image.Lesion != null &&
                                          d.Image.Lesion.PatientId == patientId);
        }

        private async Task<FinalDiagnosis?> GetFinalDiagnosis(int patientId)
        {
            return await _context.FinalDiagnoses
                .Include(fd => fd.ModelPrediction)
                .ThenInclude(d => d.Image)
                .ThenInclude(b => b.Lesion)
                .FirstOrDefaultAsync(fd => fd.ModelPrediction != null &&
                                         fd.ModelPrediction.Image != null &&
                                         fd.ModelPrediction.Image.Lesion != null &&
                                         fd.ModelPrediction.Image.Lesion.PatientId == patientId);
        }

        private async Task<Treatment?> GetTreatment(int patientId)
        {
            return await _context.Treatments
                .Include(t => t.FinalDiagnosis)
                .ThenInclude(fd => fd.ModelPrediction)
                .ThenInclude(d => d.Image)
                .ThenInclude(b => b.Lesion)
                .FirstOrDefaultAsync(t => t.FinalDiagnosis != null &&
                                        t.FinalDiagnosis.ModelPrediction != null &&
                                        t.FinalDiagnosis.ModelPrediction.Image != null &&
                                        t.FinalDiagnosis.ModelPrediction.Image.Lesion != null &&
                                        t.FinalDiagnosis.ModelPrediction.Image.Lesion.PatientId == patientId);
        }

        private async Task<FollowUp?> GetFollowUp(int patientId)
        {
            return await _context.FollowUps
                .Include(fu => fu.Treatment)
                .ThenInclude(t => t.FinalDiagnosis)
                .ThenInclude(fd => fd.ModelPrediction)
                .ThenInclude(d => d.Image)
                .ThenInclude(b => b.Lesion)
                .FirstOrDefaultAsync(fu => fu.Treatment != null &&
                                          fu.Treatment.FinalDiagnosis != null &&
                                          fu.Treatment.FinalDiagnosis.ModelPrediction != null &&
                                          fu.Treatment.FinalDiagnosis.ModelPrediction.Image != null &&
                                          fu.Treatment.FinalDiagnosis.ModelPrediction.Image.Lesion != null &&
                                          fu.Treatment.FinalDiagnosis.ModelPrediction.Image.Lesion.PatientId == patientId);
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
                Lesions = await _context.Lesions
                    .Where(l => l.PatientId == patientId)
                    .ToListAsync(),
                Images = await _context.Images
                    .Include(b => b.Lesion)
                    .Where(b => b.Lesion != null && b.Lesion.PatientId == patientId)
                    .ToListAsync(),
                ModelPredictions = await _context.ModelPredictions
                    .Include(d => d.Image)
                    .ThenInclude(b => b.Lesion)
                    .Where(d => d.Image != null && d.Image.Lesion != null && d.Image.Lesion.PatientId == patientId)
                    .ToListAsync(),
                FinalDiagnoses = await _context.FinalDiagnoses
                    .Include(fd => fd.ModelPrediction)
                    .ThenInclude(d => d.Image)
                    .ThenInclude(b => b.Lesion)
                    .Where(fd => fd.ModelPrediction != null &&
                               fd.ModelPrediction.Image != null &&
                               fd.ModelPrediction.Image.Lesion != null &&
                               fd.ModelPrediction.Image.Lesion.PatientId == patientId)
                    .ToListAsync(),
                Treatments = await _context.Treatments
                    .Include(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.ModelPrediction)
                    .ThenInclude(d => d.Image)
                    .ThenInclude(b => b.Lesion)
                    .Where(t => t.FinalDiagnosis != null &&
                               t.FinalDiagnosis.ModelPrediction != null &&
                               t.FinalDiagnosis.ModelPrediction.Image != null &&
                               t.FinalDiagnosis.ModelPrediction.Image.Lesion != null &&
                               t.FinalDiagnosis.ModelPrediction.Image.Lesion.PatientId == patientId)
                    .ToListAsync(),
                FollowUps = await _context.FollowUps
                    .Include(fu => fu.Treatment)
                    .ThenInclude(t => t.FinalDiagnosis)
                    .ThenInclude(fd => fd.ModelPrediction)
                    .ThenInclude(d => d.Image)
                    .ThenInclude(b => b.Lesion)
                    .Where(fu => fu.Treatment != null &&
                              fu.Treatment.FinalDiagnosis != null &&
                              fu.Treatment.FinalDiagnosis.ModelPrediction != null &&
                              fu.Treatment.FinalDiagnosis.ModelPrediction.Image != null &&
                              fu.Treatment.FinalDiagnosis.ModelPrediction.Image.Lesion != null &&
                              fu.Treatment.FinalDiagnosis.ModelPrediction.Image.Lesion.PatientId == patientId)
                    .ToListAsync()
            };

            return Ok(result);
        }
    }
}
