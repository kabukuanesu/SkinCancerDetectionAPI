using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Dtos.Workflows;
using SkinCancerDetectionAPI.Models;
using SkinCancerDetectionAPI.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SkinCancerDetectionAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly WorkflowService _workflowService;

        public WorkflowController(WorkflowService workflowService)
        {
            _workflowService = workflowService;
        }

        /* ========== PATIENT ENDPOINTS ========== */

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatient(int patientId)
        {
            try
            {
                var patient = await _workflowService.GetPatientByIdAsync(patientId);
                if (patient == null)
                {
                    return NotFound("Patient not found");
                }
                return Ok(patient);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("patients")]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var patients = await _workflowService.GetAllPatientsAsync(User);
                return Ok(patients);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("patient")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateDto patientDto)
        {
            try
            {
                var patient = new Patient
                {
                    UserId = patientDto.UserId,
                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    DateOfBirth = patientDto.DateOfBirth,
                    Gender = patientDto.Gender,
                    PhysicalAddress = patientDto.PhysicalAddress,
                    EmailAddress = patientDto.EmailAddress,
                    PhoneNumber = patientDto.PhoneNumber,
                    MedicalHistory = patientDto.MedicalHistory,
                    FamilyHistory = patientDto.FamilyHistory,
                    SkinType = patientDto.SkinType,
                    IsScheduled = patientDto.IsScheduled,
                    Scheduled = patientDto.Scheduled
                };

                await _workflowService.CreatePatientAsync(User, patient);
                return Ok("Patient created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("patient/{patientId}")]
        public async Task<IActionResult> UpdatePatient(int patientId, [FromBody] PatientUpdateDto patientDto)
        {
            try
            {
                var patient = new Patient
                {
                    FirstName = patientDto.FirstName,
                    LastName = patientDto.LastName,
                    DateOfBirth = patientDto.DateOfBirth,
                    Gender = patientDto.Gender,
                    PhysicalAddress = patientDto.PhysicalAddress,
                    EmailAddress = patientDto.EmailAddress,
                    PhoneNumber = patientDto.PhoneNumber,
                    MedicalHistory = patientDto.MedicalHistory,
                    FamilyHistory = patientDto.FamilyHistory,
                    SkinType = patientDto.SkinType,
                    IsScheduled = patientDto.IsScheduled,
                    Scheduled = patientDto.Scheduled
                };

                await _workflowService.UpdatePatientAsync(User, patientId, patient);
                return Ok("Patient updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== LESION ENDPOINTS ========== */

        [HttpGet("lesion/{lesionId}")]
        public async Task<IActionResult> GetLesion(int lesionId)
        {
            try
            {
                var lesion = await _workflowService.GetLesionByIdAsync(lesionId);
                if (lesion == null)
                {
                    return NotFound("Lesion not found");
                }
                return Ok(lesion);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("lesions")]
        public async Task<IActionResult> GetAllLesions()
        {
            try
            {
                var lesions = await _workflowService.GetAllLesionsAsync(User);
                return Ok(lesions);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("lesion")]
        public async Task<IActionResult> CreateLesion([FromBody] LesionCreateDto lesionDto)
        {
            try
            {
                var lesion = new Lesion
                {
                    PatientId = lesionDto.PatientId,
                    LesionLocation = lesionDto.LesionLocation,
                    LesionDescription = lesionDto.LesionDescription,
                    DermoscopicFeatures = lesionDto.DermoscopicFeatures,
                    IsBiopsy = lesionDto.IsBiopsy
                };

                await _workflowService.CreateLesionAsync(User, lesion);
                return Ok("Lesion created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("lesion/{lesionId}")]
        public async Task<IActionResult> UpdateLesion(int lesionId, [FromBody] LesionUpdateDto lesionDto)
        {
            try
            {
                var lesion = new Lesion
                {
                    LesionLocation = lesionDto.LesionLocation,
                    LesionDescription = lesionDto.LesionDescription,
                    DermoscopicFeatures = lesionDto.DermoscopicFeatures,
                    IsBiopsy = lesionDto.IsBiopsy
                };

                await _workflowService.UpdateLesionAsync(User, lesionId, lesion);
                return Ok("Lesion updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== IMAGE ENDPOINTS ========== */

        [HttpGet("image/{imageId}")]
        public async Task<IActionResult> GetImage(int imageId)
        {
            try
            {
                var image = await _workflowService.GetImageByIdAsync(imageId);
                if (image == null)
                {
                    return NotFound("Image not found");
                }
                return Ok(image);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("images")]
        public async Task<IActionResult> GetAllImages()
        {
            try
            {
                var images = await _workflowService.GetAllImagesAsync(User);
                return Ok(images);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("image")]
        public async Task<IActionResult> CreateImage([FromBody] ImageCreateDto imageDto)
        {
            try
            {
                var image = new Image
                {
                    LesionId = imageDto.LesionId,
                    ImageType = imageDto.ImageType,
                    ImageDescription = imageDto.ImageDescription,
                    Resolution = imageDto.Resolution,
                    CameraModel = imageDto.CameraModel,
                    IlluminationType = imageDto.IlluminationType
                };

                await _workflowService.CreateImageAsync(User, image);
                return Ok("Image created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("image/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromBody] ImageUpdateDto imageDto)
        {
            try
            {
                var existingImage = await _workflowService.GetImageByIdAsync(imageId);
                if (existingImage == null)
                {
                    return NotFound("Image not found");
                }

                var image = new Image
                {
                    ImageType = imageDto.ImageType,
                    ImageDescription = imageDto.ImageDescription,
                    Resolution = imageDto.Resolution,
                    CameraModel = imageDto.CameraModel,
                    IlluminationType = imageDto.IlluminationType
                };

                await _workflowService.UpdateImageAsync(User, image);
                return Ok("Image updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== PREDICTION ENDPOINTS ========== */

        [HttpGet("prediction/{predictionId}")]
        public async Task<IActionResult> GetPrediction(int predictionId)
        {
            try
            {
                var prediction = await _workflowService.GetPredictionByIdAsync(predictionId);
                if (prediction == null)
                {
                    return NotFound("Prediction not found");
                }
                return Ok(prediction);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("predictions")]
        public async Task<IActionResult> GetAllPredictions()
        {
            try
            {
                var predictions = await _workflowService.GetAllPredictionsAsync(User);
                return Ok(predictions);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("prediction")]
        public async Task<IActionResult> CreatePrediction([FromBody] ModelPredictionCreateDto predictionDto)
        {
            try
            {
                var prediction = new ModelPrediction
                {
                    ImageId = predictionDto.ImageId,
                    PredictionResult = predictionDto.PredictionResult,
                    ConfidenceScore = predictionDto.ConfidenceScore
                };

                await _workflowService.CreatePredictionAsync(User, prediction);
                return Ok("Prediction created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("prediction/{predictionId}")]
        public async Task<IActionResult> UpdatePrediction(int predictionId, [FromBody] ModelPredictionUpdateDto predictionDto)
        {
            try
            {
                var existingPrediction = await _workflowService.GetPredictionByIdAsync(predictionId);
                if (existingPrediction == null)
                {
                    return NotFound("Prediction not found");
                }

                var prediction = new ModelPrediction
                {
                    PredictionResult = predictionDto.PredictionResult,
                    ConfidenceScore = predictionDto.ConfidenceScore
                };

                await _workflowService.UpdatePredictionAsync(User, prediction);
                return Ok("Prediction updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== BIOPSY ENDPOINTS ========== */

        [HttpGet("biopsy/{biopsyId}")]
        public async Task<IActionResult> GetBiopsy(int biopsyId)
        {
            try
            {
                var biopsy = await _workflowService.GetBiopsyByIdAsync(biopsyId);
                if (biopsy == null)
                {
                    return NotFound("Biopsy not found");
                }
                return Ok(biopsy);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("biopsies")]
        public async Task<IActionResult> GetAllBiopsies()
        {
            try
            {
                var biopsies = await _workflowService.GetAllBiopsiesAsync(User);
                return Ok(biopsies);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("biopsy")]
        public async Task<IActionResult> CreateBiopsy([FromBody] BiopsyCreateDto biopsyDto)
        {
            try
            {
                var biopsy = new Biopsy
                {
                    LesionId = biopsyDto.LesionId,
                    BiopsyResult = biopsyDto.BiopsyResult,
                    BiopsyResultDescription = biopsyDto.BiopsyResultDescription
                };

                await _workflowService.CreateBiopsyAsync(User, biopsy);
                return Ok("Biopsy created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("biopsy/{biopsyId}")]
        public async Task<IActionResult> UpdateBiopsy(int biopsyId, [FromBody] BiopsyUpdateDto biopsyDto)
        {
            try
            {
                var existingBiopsy = await _workflowService.GetBiopsyByIdAsync(biopsyId);
                if (existingBiopsy == null)
                {
                    return NotFound("Biopsy not found");
                }

                var biopsy = new Biopsy
                {
                    BiopsyResult = biopsyDto.BiopsyResult,
                    BiopsyResultDescription = biopsyDto.BiopsyResultDescription
                };

                await _workflowService.UpdateBiopsyAsync(User, biopsy);
                return Ok("Biopsy updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== DIAGNOSIS ENDPOINTS ========== */

        [HttpGet("diagnosis/{diagnosisId}")]
        public async Task<IActionResult> GetDiagnosis(int diagnosisId)
        {
            try
            {
                var diagnosis = await _workflowService.GetDiagnosisByIdAsync(diagnosisId);
                if (diagnosis == null)
                {
                    return NotFound("Diagnosis not found");
                }
                return Ok(diagnosis);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("diagnoses")]
        public async Task<IActionResult> GetAllDiagnoses()
        {
            try
            {
                var diagnoses = await _workflowService.GetAllDiagnosesAsync(User);
                return Ok(diagnoses);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("diagnosis")]
        public async Task<IActionResult> CreateDiagnosis([FromBody] DiagnosisCreateDto diagnosisDto)
        {
            try
            {
                var diagnosis = new Diagnosis
                {
                    BiopsyId = diagnosisDto.BiopsyId,
                    DiagnosisResult = diagnosisDto.DiagnosisResult,
                    DiagnosisDescription = diagnosisDto.DiagnosisDescription
                };

                await _workflowService.CreateDiagnosisAsync(User, diagnosis);
                return Ok("Diagnosis created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("diagnosis/{diagnosisId}")]
        public async Task<IActionResult> UpdateDiagnosis(int diagnosisId, [FromBody] DiagnosisUpdateDto diagnosisDto)
        {
            try
            {
                var existingDiagnosis = await _workflowService.GetDiagnosisByIdAsync(diagnosisId);
                if (existingDiagnosis == null)
                {
                    return NotFound("Diagnosis not found");
                }

                var diagnosis = new Diagnosis
                {
                    DiagnosisResult = diagnosisDto.DiagnosisResult,
                    DiagnosisDescription = diagnosisDto.DiagnosisDescription
                };

                await _workflowService.UpdateDiagnosisAsync(User, diagnosis);
                return Ok("Diagnosis updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== FINAL DIAGNOSIS ENDPOINTS ========== */

        [HttpGet("finaldiagnosis/{finalDiagnosisId}")]
        public async Task<IActionResult> GetFinalDiagnosis(int finalDiagnosisId)
        {
            try
            {
                var finalDiagnosis = await _workflowService.GetFinalDiagnosisByIdAsync(finalDiagnosisId);
                if (finalDiagnosis == null)
                {
                    return NotFound("Final diagnosis not found");
                }
                return Ok(finalDiagnosis);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("finaldiagnoses")]
        public async Task<IActionResult> GetAllFinalDiagnoses()
        {
            try
            {
                var finalDiagnoses = await _workflowService.GetAllFinalDiagnosesAsync(User);
                return Ok(finalDiagnoses);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("finaldiagnosis")]
        public async Task<IActionResult> CreateFinalDiagnosis([FromBody] FinalDiagnosisCreateDto finalDiagnosisDto)
        {
            try
            {
                var finalDiagnosis = new FinalDiagnosis
                {
                    DiagnosisId = finalDiagnosisDto.DiagnosisId,
                    ModelPredictionId = finalDiagnosisDto.ModelPredictionId,
                    FinalResult = finalDiagnosisDto.FinalResult,
                    FinalResultDescription = finalDiagnosisDto.FinalResultDescription
                };

                await _workflowService.CreateFinalDiagnosisAsync(User, finalDiagnosis);
                return Ok("Final diagnosis created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("finaldiagnosis/{finalDiagnosisId}")]
        public async Task<IActionResult> UpdateFinalDiagnosis(int finalDiagnosisId, [FromBody] FinalDiagnosisUpdateDto finalDiagnosisDto)
        {
            try
            {
                var existingFinalDiagnosis = await _workflowService.GetFinalDiagnosisByIdAsync(finalDiagnosisId);
                if (existingFinalDiagnosis == null)
                {
                    return NotFound("Final diagnosis not found");
                }

                var finalDiagnosis = new FinalDiagnosis
                {
                    FinalResult = finalDiagnosisDto.FinalResult,
                    FinalResultDescription = finalDiagnosisDto.FinalResultDescription
                };

                await _workflowService.UpdateFinalDiagnosisAsync(User, finalDiagnosis);
                return Ok("Final diagnosis updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== TREATMENT ENDPOINTS ========== */

        [HttpGet("treatment/{treatmentId}")]
        public async Task<IActionResult> GetTreatment(int treatmentId)
        {
            try
            {
                var treatment = await _workflowService.GetTreatmentByIdAsync(treatmentId);
                if (treatment == null)
                {
                    return NotFound("Treatment not found");
                }
                return Ok(treatment);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("treatments")]
        public async Task<IActionResult> GetAllTreatments()
        {
            try
            {
                var treatments = await _workflowService.GetAllTreatmentsAsync(User);
                return Ok(treatments);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("treatment")]
        public async Task<IActionResult> CreateTreatment([FromBody] TreatmentCreateDto treatmentDto)
        {
            try
            {
                var treatment = new Treatment
                {
                    FinalDiagnosisId = treatmentDto.FinalDiagnosisId,
                    TreatmentType = treatmentDto.TreatmentType,
                    TreatmentDescription = treatmentDto.TreatmentDescription,
                    IsFollowUp = treatmentDto.IsFollowUp
                };

                await _workflowService.CreateTreatmentAsync(User, treatment);
                return Ok("Treatment created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("treatment/{treatmentId}")]
        public async Task<IActionResult> UpdateTreatment(int treatmentId, [FromBody] TreatmentUpdateDto treatmentDto)
        {
            try
            {
                var existingTreatment = await _workflowService.GetTreatmentByIdAsync(treatmentId);
                if (existingTreatment == null)
                {
                    return NotFound("Treatment not found");
                }

                var treatment = new Treatment
                {
                    TreatmentType = treatmentDto.TreatmentType,
                    TreatmentDescription = treatmentDto.TreatmentDescription,
                    IsFollowUp = treatmentDto.IsFollowUp
                };

                await _workflowService.UpdateTreatmentAsync(User, treatment);
                return Ok("Treatment updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== FOLLOW-UP ENDPOINTS ========== */

        [HttpGet("followup/{followUpId}")]
        public async Task<IActionResult> GetFollowUp(int followUpId)
        {
            try
            {
                var followUp = await _workflowService.GetFollowUpByIdAsync(followUpId);
                if (followUp == null)
                {
                    return NotFound("Follow-up not found");
                }
                return Ok(followUp);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("followups")]
        public async Task<IActionResult> GetAllFollowUps()
        {
            try
            {
                var followUps = await _workflowService.GetAllFollowUpsAsync(User);
                return Ok(followUps);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("followup")]
        public async Task<IActionResult> CreateFollowUp([FromBody] FollowUpCreateDto followUpDto)
        {
            try
            {
                var followUp = new FollowUp
                {
                    TreatmentId = followUpDto.TreatmentId,
                    FollowUpResult = followUpDto.FollowUpResult,
                    FollowUpDescription = followUpDto.FollowUpDescription,
                    IsFollowUp = followUpDto.IsFollowUp
                };

                await _workflowService.CreateFollowUpAsync(User, followUp);
                return Ok("Follow-up created successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("followup/{followUpId}")]
        public async Task<IActionResult> UpdateFollowUp(int followUpId, [FromBody] FollowUpUpdateDto followUpDto)
        {
            try
            {
                var existingFollowUp = await _workflowService.GetFollowUpByIdAsync(followUpId);
                if (existingFollowUp == null)
                {
                    return NotFound("Follow-up not found");
                }

                var followUp = new FollowUp
                {
                    FollowUpResult = followUpDto.FollowUpResult,
                    FollowUpDescription = followUpDto.FollowUpDescription,
                    IsFollowUp = followUpDto.IsFollowUp
                };

                await _workflowService.UpdateFollowUpAsync(User, followUp);
                return Ok("Follow-up updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== WORKFLOW MANAGEMENT ========== */

        [HttpPost("start")]
        public async Task<IActionResult> StartWorkflow(int patientId)
        {
            try
            {
                var workflowId = await _workflowService.StartWorkflowAsync(User, patientId);
                return Ok(new { WorkflowId = workflowId, Message = "Workflow started successfully." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("save")]
        public async Task<IActionResult> SaveWorkflow(int workflowId, string currentStage)
        {
            try
            {
                await _workflowService.SaveWorkflowAsync(User, workflowId, currentStage);
                return Ok("Workflow saved successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("resume")]
        public async Task<IActionResult> ResumeWorkflow(int workflowId)
        {
            try
            {
                await _workflowService.ResumeWorkflowAsync(User, workflowId);
                return Ok("Workflow resumed successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /* ========== WORKFLOW GET ENDPOINTS ========== */

        [HttpGet("workflows")]
        public async Task<IActionResult> GetAllWorkflows()
        {
            try
            {
                var workflows = await _workflowService.GetAllWorkflowsAsync(User);
                return Ok(workflows);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("workflow/{workflowId}")]
        public async Task<IActionResult> GetWorkflow(int workflowId)
        {
            try
            {
                var workflow = await _workflowService.GetWorkflowByIdAsync(workflowId);
                if (workflow == null)
                {
                    return NotFound("Workflow not found");
                }
                return Ok(workflow);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
