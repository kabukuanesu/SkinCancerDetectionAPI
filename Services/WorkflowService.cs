using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Dtos.Workflows;
using SkinCancerDetectionAPI.Models;
using System;
using System.Collections.Generic;
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

        // Stage 1: Patient Management (Admin and Dermatologist)
        public async Task CreatePatientAsync(ClaimsPrincipal user, Patient patient)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can create patients.");
            }

            patient.DateCreated = DateTime.UtcNow;
            patient.DateUpdated = DateTime.UtcNow;
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(ClaimsPrincipal user, int patientId, Patient patient)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can update patients.");
            }

            var existingPatient = await _context.Patients.FindAsync(patientId);
            if (existingPatient == null)
            {
                throw new ArgumentException("Patient not found");
            }

            // Update all properties
            existingPatient.FirstName = patient.FirstName;
            existingPatient.LastName = patient.LastName;
            existingPatient.DateOfBirth = patient.DateOfBirth;
            existingPatient.Gender = patient.Gender;
            existingPatient.PhysicalAddress = patient.PhysicalAddress;
            existingPatient.EmailAddress = patient.EmailAddress;
            existingPatient.PhoneNumber = patient.PhoneNumber;
            existingPatient.MedicalHistory = patient.MedicalHistory;
            existingPatient.FamilyHistory = patient.FamilyHistory;
            existingPatient.SkinType = patient.SkinType;
            existingPatient.IsScheduled = patient.IsScheduled;
            existingPatient.Scheduled = patient.Scheduled;
            existingPatient.DateUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        // Stage 2: Lesion Management (Only Dermatologist)
        public async Task CreateLesionAsync(ClaimsPrincipal user, Lesion lesion)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can create lesions.");
            }

            lesion.DateOfDetection = DateTime.UtcNow;
            lesion.DateOfExamination = DateTime.UtcNow;
            _context.Lesions.Add(lesion);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLesionAsync(ClaimsPrincipal user, int lesionId, Lesion lesion)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can update lesions.");
            }

            var existingLesion = await _context.Lesions.FindAsync(lesionId);
            if (existingLesion == null)
            {
                throw new ArgumentException("Lesion not found");
            }

            existingLesion.LesionLocation = lesion.LesionLocation;
            existingLesion.LesionDescription = lesion.LesionDescription;
            existingLesion.DermoscopicFeatures = lesion.DermoscopicFeatures;
            existingLesion.IsBiopsy = lesion.IsBiopsy;

            await _context.SaveChangesAsync();
        }

        // Stage 3: Image and Prediction Management (Dermatologist, Doctor, Nurse)
        public async Task CreateImageAsync(ClaimsPrincipal user, Image image)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can create images.");
            }

            image.DateTaken = DateTime.UtcNow;
            image.DateUpdated = DateTime.UtcNow;
            _context.Images.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePredictionAsync(ClaimsPrincipal user, ModelPrediction prediction)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can create predictions.");
            }

            _context.ModelPredictions.Add(prediction);
            await _context.SaveChangesAsync();
        }

        // Stage 4: Biopsy and Diagnosis Management (Dermatologist, Doctor, Nurse)
        public async Task CreateBiopsyAsync(ClaimsPrincipal user, Biopsy biopsy)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can create biopsies.");
            }

            biopsy.BiopsyDate = DateTime.UtcNow;
            _context.Biopsies.Add(biopsy);
            await _context.SaveChangesAsync();
        }

        public async Task CreateDiagnosisAsync(ClaimsPrincipal user, Diagnosis diagnosis)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can create diagnoses.");
            }

            diagnosis.DiagnosisDate = DateTime.UtcNow;
            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();
        }

        // Stage 5: Final Diagnosis and Treatment Management (Only Dermatologist)
        public async Task CreateFinalDiagnosisAsync(ClaimsPrincipal user, FinalDiagnosis finalDiagnosis)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can create final diagnoses.");
            }

            _context.FinalDiagnoses.Add(finalDiagnosis);
            await _context.SaveChangesAsync();
        }

        public async Task CreateTreatmentAsync(ClaimsPrincipal user, Treatment treatment)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can create treatments.");
            }

            treatment.TreatmentDate = DateTime.UtcNow;
            treatment.FollowUpDate = DateTime.UtcNow.AddDays(30);
            _context.Treatments.Add(treatment);
            await _context.SaveChangesAsync();
        }

        // Stage 6: FollowUp Management (Admin and Dermatologist)
        public async Task CreateFollowUpAsync(ClaimsPrincipal user, FollowUp followUp)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can create follow-ups.");
            }

            followUp.FollowUpDate = DateTime.UtcNow;
            followUp.FollowingUpDate = DateTime.UtcNow.AddDays(30);
            _context.FollowUps.Add(followUp);
            await _context.SaveChangesAsync();
        }

        // Workflow Management
        public async Task<int> StartWorkflowAsync(ClaimsPrincipal user, int patientId)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can start workflows.");
            }

            var workflow = new Workflow
            {
                PatientId = patientId,
                Status = "Started",
                CreatedBy = user.Identity?.Name ?? "System",
                DateCreated = DateTime.UtcNow,
                DateUpdated = DateTime.UtcNow
            };

            _context.Workflows.Add(workflow);
            await _context.SaveChangesAsync();

            return workflow.WorkflowId;
        }

        public async Task SaveWorkflowAsync(ClaimsPrincipal user, int workflowId, string currentStage)
        {
            var workflow = await _context.Workflows.FindAsync(workflowId);
            if (workflow == null)
            {
                throw new ArgumentException("Workflow not found");
            }

            workflow.Status = "Saved";
            workflow.DateUpdated = DateTime.UtcNow;

            var stage = new WorkflowStage
            {
                WorkflowId = workflowId,
                WorkflowStageName = currentStage,
                Status = "Saved",
                AssignedTo = user.Identity?.Name ?? "System",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            };

            _context.WorkflowStages.Add(stage);
            await _context.SaveChangesAsync();
        }

        public async Task ResumeWorkflowAsync(ClaimsPrincipal user, int workflowId)
        {
            var workflow = await _context.Workflows.FindAsync(workflowId);
            if (workflow == null)
            {
                throw new ArgumentException("Workflow not found");
            }

            workflow.Status = "In Progress";
            workflow.DateUpdated = DateTime.UtcNow;

            var lastStage = await _context.WorkflowStages
                .Where(ws => ws.WorkflowId == workflowId)
                .OrderByDescending(ws => ws.WorkflowStageId)
                .FirstOrDefaultAsync();

            if (lastStage != null)
            {
                lastStage.Status = "Resumed";
                lastStage.EndDate = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateImageAsync(ClaimsPrincipal user, Image image)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can update images.");
            }

            _context.Images.Update(image);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePredictionAsync(ClaimsPrincipal user, ModelPrediction prediction)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can update predictions.");
            }

            _context.ModelPredictions.Update(prediction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBiopsyAsync(ClaimsPrincipal user, Biopsy biopsy)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can update biopsies.");
            }

            _context.Biopsies.Update(biopsy);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDiagnosisAsync(ClaimsPrincipal user, Diagnosis diagnosis)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can update diagnoses.");
            }

            _context.Diagnoses.Update(diagnosis);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFinalDiagnosisAsync(ClaimsPrincipal user, FinalDiagnosis finalDiagnosis)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can update final diagnoses.");
            }

            _context.FinalDiagnoses.Update(finalDiagnosis);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTreatmentAsync(ClaimsPrincipal user, Treatment treatment)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can update treatments.");
            }

            _context.Treatments.Update(treatment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFollowUpAsync(ClaimsPrincipal user, FollowUp followUp)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can update follow-ups.");
            }

            _context.FollowUps.Update(followUp);
            await _context.SaveChangesAsync();
        }

        // Patient methods
        public async Task<PatientResponseDto?> GetPatientByIdAsync(int patientId)
        {
            var patient = await _context.Patients.FindAsync(patientId);
            if (patient == null) return null;

            return new PatientResponseDto
            {
                PatientId = patient.PatientId,
                UserId = patient.UserId,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                DateOfBirth = patient.DateOfBirth,
                Gender = patient.Gender,
                PhysicalAddress = patient.PhysicalAddress,
                EmailAddress = patient.EmailAddress,
                PhoneNumber = patient.PhoneNumber,
                MedicalHistory = patient.MedicalHistory,
                FamilyHistory = patient.FamilyHistory,
                SkinType = patient.SkinType,
                IsScheduled = patient.IsScheduled,
                Scheduled = patient.Scheduled,
                DateCreated = patient.DateCreated,
                DateUpdated = patient.DateUpdated
            };
        }

        public async Task<List<PatientResponseDto>> GetAllPatientsAsync(ClaimsPrincipal user)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can view patients.");
            }

            var patients = await _context.Patients.ToListAsync();
            return patients.Select(p => new PatientResponseDto
            {
                PatientId = p.PatientId,
                UserId = p.UserId,
                FirstName = p.FirstName,
                LastName = p.LastName,
                DateOfBirth = p.DateOfBirth,
                Gender = p.Gender,
                PhysicalAddress = p.PhysicalAddress,
                EmailAddress = p.EmailAddress,
                PhoneNumber = p.PhoneNumber,
                MedicalHistory = p.MedicalHistory,
                FamilyHistory = p.FamilyHistory,
                SkinType = p.SkinType,
                IsScheduled = p.IsScheduled,
                Scheduled = p.Scheduled,
                DateCreated = p.DateCreated,
                DateUpdated = p.DateUpdated
            }).ToList();
        }

        // Lesion methods
        public async Task<LesionResponseDto?> GetLesionByIdAsync(int lesionId)
        {
            var lesion = await _context.Lesions.FindAsync(lesionId);
            if (lesion == null) return null;

            return new LesionResponseDto
            {
                LesionId = lesion.LesionId,
                PatientId = lesion.PatientId,
                LesionLocation = lesion.LesionLocation,
                DateOfDetection = lesion.DateOfDetection,
                DateOfExamination = lesion.DateOfExamination,
                LesionDescription = lesion.LesionDescription,
                DermoscopicFeatures = lesion.DermoscopicFeatures,
                IsBiopsy = lesion.IsBiopsy
            };
        }

        public async Task<List<LesionResponseDto>> GetAllLesionsAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can view lesions.");
            }

            var lesions = await _context.Lesions.ToListAsync();
            return lesions.Select(l => new LesionResponseDto
            {
                LesionId = l.LesionId,
                PatientId = l.PatientId,
                LesionLocation = l.LesionLocation,
                DateOfDetection = l.DateOfDetection,
                DateOfExamination = l.DateOfExamination,
                LesionDescription = l.LesionDescription,
                DermoscopicFeatures = l.DermoscopicFeatures,
                IsBiopsy = l.IsBiopsy
            }).ToList();
        }

        // Image methods
        public async Task<ImageResponseDto?> GetImageByIdAsync(int imageId)
        {
            var image = await _context.Images.FindAsync(imageId);
            if (image == null) return null;

            return new ImageResponseDto
            {
                ImageId = image.ImageId,
                LesionId = image.LesionId,
                ImageType = image.ImageType,
                ImageDescription = image.ImageDescription,
                Resolution = image.Resolution,
                DateTaken = image.DateTaken,
                CameraModel = image.CameraModel,
                IlluminationType = image.IlluminationType,
                DateUpdated = image.DateUpdated
            };
        }

        public async Task<List<ImageResponseDto>> GetAllImagesAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can view images.");
            }

            var images = await _context.Images.ToListAsync();
            return images.Select(i => new ImageResponseDto
            {
                ImageId = i.ImageId,
                LesionId = i.LesionId,
                ImageType = i.ImageType,
                ImageDescription = i.ImageDescription,
                Resolution = i.Resolution,
                DateTaken = i.DateTaken,
                CameraModel = i.CameraModel,
                IlluminationType = i.IlluminationType,
                DateUpdated = i.DateUpdated
            }).ToList();
        }

        // ModelPrediction methods
        public async Task<ModelPredictionResponseDto?> GetPredictionByIdAsync(int predictionId)
        {
            var prediction = await _context.ModelPredictions.FindAsync(predictionId);
            if (prediction == null) return null;

            return new ModelPredictionResponseDto
            {
                ModelPredictionId = prediction.ModelPredictionId,
                ImageId = prediction.ImageId,
                PredictionResult = prediction.PredictionResult,
                ConfidenceScore = prediction.ConfidenceScore
            };
        }

        public async Task<List<ModelPredictionResponseDto>> GetAllPredictionsAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can view predictions.");
            }

            var predictions = await _context.ModelPredictions.ToListAsync();
            return predictions.Select(p => new ModelPredictionResponseDto
            {
                ModelPredictionId = p.ModelPredictionId,
                ImageId = p.ImageId,
                PredictionResult = p.PredictionResult,
                ConfidenceScore = p.ConfidenceScore
            }).ToList();
        }

        // Biopsy methods
        public async Task<BiopsyResponseDto?> GetBiopsyByIdAsync(int biopsyId)
        {
            var biopsy = await _context.Biopsies.FindAsync(biopsyId);
            if (biopsy == null) return null;

            return new BiopsyResponseDto
            {
                BiopsyId = biopsy.BiopsyId,
                LesionId = biopsy.LesionId,
                BiopsyDate = biopsy.BiopsyDate,
                BiopsyResult = biopsy.BiopsyResult,
                BiopsyResultDescription = biopsy.BiopsyResultDescription
            };
        }

        public async Task<List<BiopsyResponseDto>> GetAllBiopsiesAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can view biopsies.");
            }

            var biopsies = await _context.Biopsies.ToListAsync();
            return biopsies.Select(b => new BiopsyResponseDto
            {
                BiopsyId = b.BiopsyId,
                LesionId = b.LesionId,
                BiopsyDate = b.BiopsyDate,
                BiopsyResult = b.BiopsyResult,
                BiopsyResultDescription = b.BiopsyResultDescription
            }).ToList();
        }

        // Diagnosis methods
        public async Task<DiagnosisResponseDto?> GetDiagnosisByIdAsync(int diagnosisId)
        {
            var diagnosis = await _context.Diagnoses.FindAsync(diagnosisId);
            if (diagnosis == null) return null;

            return new DiagnosisResponseDto
            {
                DiagnosisId = diagnosis.DiagnosisId,
                BiopsyId = diagnosis.BiopsyId,
                DiagnosisDate = diagnosis.DiagnosisDate,
                DiagnosisResult = diagnosis.DiagnosisResult,
                DiagnosisDescription = diagnosis.DiagnosisDescription
            };
        }

        public async Task<List<DiagnosisResponseDto>> GetAllDiagnosesAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");
            var isDoctor = user.IsInRole("Doctor");
            var isNurse = user.IsInRole("Nurse");

            if (!isDermatologist && !isDoctor && !isNurse)
            {
                throw new UnauthorizedAccessException("Only Dermatologist, Doctor, and Nurse can view diagnoses.");
            }

            var diagnoses = await _context.Diagnoses.ToListAsync();
            return diagnoses.Select(d => new DiagnosisResponseDto
            {
                DiagnosisId = d.DiagnosisId,
                BiopsyId = d.BiopsyId,
                DiagnosisDate = d.DiagnosisDate,
                DiagnosisResult = d.DiagnosisResult,
                DiagnosisDescription = d.DiagnosisDescription
            }).ToList();
        }

        // FinalDiagnosis methods
        public async Task<FinalDiagnosisResponseDto?> GetFinalDiagnosisByIdAsync(int finalDiagnosisId)
        {
            var finalDiagnosis = await _context.FinalDiagnoses.FindAsync(finalDiagnosisId);
            if (finalDiagnosis == null) return null;

            return new FinalDiagnosisResponseDto
            {
                FinalDiagnosisId = finalDiagnosis.FinalDiagnosisId,
                DiagnosisId = finalDiagnosis.DiagnosisId,
                ModelPredictionId = finalDiagnosis.ModelPredictionId,
                FinalResult = finalDiagnosis.FinalResult,
                FinalResultDescription = finalDiagnosis.FinalResultDescription
            };
        }

        public async Task<List<FinalDiagnosisResponseDto>> GetAllFinalDiagnosesAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can view final diagnoses.");
            }

            var finalDiagnoses = await _context.FinalDiagnoses.ToListAsync();
            return finalDiagnoses.Select(fd => new FinalDiagnosisResponseDto
            {
                FinalDiagnosisId = fd.FinalDiagnosisId,
                DiagnosisId = fd.DiagnosisId,
                ModelPredictionId = fd.ModelPredictionId,
                FinalResult = fd.FinalResult,
                FinalResultDescription = fd.FinalResultDescription
            }).ToList();
        }

        // Treatment methods
        public async Task<TreatmentResponseDto?> GetTreatmentByIdAsync(int treatmentId)
        {
            var treatment = await _context.Treatments.FindAsync(treatmentId);
            if (treatment == null) return null;

            return new TreatmentResponseDto
            {
                TreatmentId = treatment.TreatmentId,
                FinalDiagnosisId = treatment.FinalDiagnosisId,
                TreatmentDate = treatment.TreatmentDate,
                TreatmentType = treatment.TreatmentType,
                TreatmentDescription = treatment.TreatmentDescription,
                IsFollowUp = treatment.IsFollowUp,
                FollowUpDate = treatment.FollowUpDate
            };
        }

        public async Task<List<TreatmentResponseDto>> GetAllTreatmentsAsync(ClaimsPrincipal user)
        {
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Dermatologist can view treatments.");
            }

            var treatments = await _context.Treatments.ToListAsync();
            return treatments.Select(t => new TreatmentResponseDto
            {
                TreatmentId = t.TreatmentId,
                FinalDiagnosisId = t.FinalDiagnosisId,
                TreatmentDate = t.TreatmentDate,
                TreatmentType = t.TreatmentType,
                TreatmentDescription = t.TreatmentDescription,
                IsFollowUp = t.IsFollowUp,
                FollowUpDate = t.FollowUpDate
            }).ToList();
        }

        // FollowUp methods
        public async Task<FollowUpResponseDto?> GetFollowUpByIdAsync(int followUpId)
        {
            var followUp = await _context.FollowUps.FindAsync(followUpId);
            if (followUp == null) return null;

            return new FollowUpResponseDto
            {
                FollowUpId = followUp.FollowUpId,
                TreatmentId = followUp.TreatmentId,
                FollowUpDate = followUp.FollowUpDate,
                FollowUpResult = followUp.FollowUpResult,
                FollowUpDescription = followUp.FollowUpDescription,
                IsFollowUp = followUp.IsFollowUp,
                FollowingUpDate = followUp.FollowingUpDate
            };
        }

        public async Task<List<FollowUpResponseDto>> GetAllFollowUpsAsync(ClaimsPrincipal user)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can view follow-ups.");
            }

            var followUps = await _context.FollowUps.ToListAsync();
            return followUps.Select(f => new FollowUpResponseDto
            {
                FollowUpId = f.FollowUpId,
                TreatmentId = f.TreatmentId,
                FollowUpDate = f.FollowUpDate,
                FollowUpResult = f.FollowUpResult,
                FollowUpDescription = f.FollowUpDescription,
                IsFollowUp = f.IsFollowUp,
                FollowingUpDate = f.FollowingUpDate
            }).ToList();
        }

        // Workflow methods
        public async Task<WorkflowResponseDto?> GetWorkflowByIdAsync(int workflowId)
        {
            var workflow = await _context.Workflows.FindAsync(workflowId);
            if (workflow == null) return null;

            return new WorkflowResponseDto
            {
                WorkflowId = workflow.WorkflowId,
                PatientId = workflow.PatientId,
                Status = workflow.Status,
                CreatedBy = workflow.CreatedBy,
                DateCreated = workflow.DateCreated,
                DateUpdated = workflow.DateUpdated
            };
        }

        public async Task<List<WorkflowResponseDto>> GetAllWorkflowsAsync(ClaimsPrincipal user)
        {
            var isAdmin = user.IsInRole("Admin");
            var isDermatologist = user.IsInRole("Dermatologist");

            if (!isAdmin && !isDermatologist)
            {
                throw new UnauthorizedAccessException("Only Admin and Dermatologist can view workflows.");
            }

            var workflows = await _context.Workflows.ToListAsync();
            return workflows.Select(w => new WorkflowResponseDto
            {
                WorkflowId = w.WorkflowId,
                PatientId = w.PatientId,
                Status = w.Status,
                CreatedBy = w.CreatedBy,
                DateCreated = w.DateCreated,
                DateUpdated = w.DateUpdated
            }).ToList();
        }
    }
}
