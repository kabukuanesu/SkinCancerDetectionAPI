using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkinCancerDetectionAPI.Services
{
    public class WorkflowService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WorkflowService(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Stage 1: Admin can manipulate Patients, Dermatologist can manipulate Patients and Lesions
        public async Task StartWorkflowAsync(ClaimsPrincipal user, int patientId, string lesionLocation, string lesionDescription)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can start the workflow.");
            }

            // Update Patient table (if needed)
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new ArgumentException("Patient not found");
            }

            // Update Lesion table (only Dermatologist can add lesions)
            if (isDermatologist)
            {
                var lesion = new Lesion
                {
                    PatientId = patientId,
                    LesionLocation = lesionLocation,
                    LesionDescription = lesionDescription,
                    DateOfDetection = DateTime.UtcNow
                };
                _context.Lesions.Add(lesion);
            }

            await _context.SaveChangesAsync();
        }

        // Stage 2: Dermatologist, Doctor, and Nurse can manipulate Images and ModelPredictions
        public async Task UpdateImageAndPredictionAsync(ClaimsPrincipal user, int lesionId, string imageType, string predictionResult, float confidenceScore)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can update images and predictions.");
            }

            // Update Image table
            var image = new Image
            {
                LesionId = lesionId,
                ImageType = imageType,
                ImageDescription = "Image description",
                Resolution = "1920x1080",
                DateTaken = DateTime.UtcNow,
                CameraModel = "Camera Model",
                IlluminationType = "Natural Light",
                DateUpdated = DateTime.UtcNow
            };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            // Update ModelPrediction table
            var prediction = new ModelPrediction
            {
                ImageId = image.ImageId,
                PredictionResult = predictionResult,
                ConfidenceScore = confidenceScore
            };
            _context.ModelPredictions.Add(prediction);
            await _context.SaveChangesAsync();
        }

        // Stage 3: Dermatologist, Doctor, and Nurse can manipulate Biopsies and Diagnoses
        public async Task UpdateBiopsyAndDiagnosisAsync(ClaimsPrincipal user, int lesionId, string biopsyResult, string diagnosisResult)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can update biopsies and diagnoses.");
            }

            // Update Biopsy table
            var biopsy = new Biopsy
            {
                LesionId = lesionId,
                BiopsyDate = DateTime.UtcNow,
                BiopsyResult = biopsyResult,
                BiopsyResultDescription = "Biopsy result description"
            };
            _context.Biopsies.Add(biopsy);
            await _context.SaveChangesAsync();

            // Update Diagnosis table
            var diagnosis = new Diagnosis
            {
                BiopsyId = biopsy.BiopsyId,
                DiagnosisDate = DateTime.UtcNow,
                DiagnosisResult = diagnosisResult,
                DiagnosisDescription = "Diagnosis description"
            };
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();
        }

        // Stage 4: Only Dermatologist can manipulate FinalDiagnoses and Treatments
        public async Task UpdateFinalDiagnosisAndTreatmentAsync(ClaimsPrincipal user, int diagnosisId, int modelPredictionId, string finalResult, string treatmentType)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can update final diagnoses and treatments.");
            }

            // Update FinalDiagnosis table
            var finalDiagnosis = new FinalDiagnosis
            {
                DiagnosisId = diagnosisId,
                ModelPredictionId = modelPredictionId,
                FinalResult = finalResult,
                FinalResultDescription = "Final diagnosis description"
            };
            _context.FinalDiagnoses.Add(finalDiagnosis);
            await _context.SaveChangesAsync();

            // Update Treatment table
            var treatment = new Treatment
            {
                FinalDiagnosisId = finalDiagnosis.FinalDiagnosisId,
                TreatmentDate = DateTime.UtcNow,
                TreatmentType = treatmentType,
                TreatmentDescription = "Treatment description",
                IsFollowUp = true,
                FollowUpDate = DateTime.UtcNow.AddDays(30)
            };
            _context.Treatments.Add(treatment);
            await _context.SaveChangesAsync();
        }
    }
}
