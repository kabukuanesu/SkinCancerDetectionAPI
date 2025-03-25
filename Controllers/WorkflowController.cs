using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        // Stage 1: Admin and Dermatologist can start the workflow
        [HttpPost("start")]
        public async Task<IActionResult> StartWorkflow(int patientId, string lesionLocation, string lesionDescription, string dermoscopicFeatures, bool isBiopsy)
        {
            try
            {
                await _workflowService.StartWorkflowAsync(User, patientId, lesionLocation, lesionDescription, dermoscopicFeatures, isBiopsy);
                return Ok("Workflow started successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Stage 2: Dermatologist, Doctor, and Nurse can update images and predictions
        [HttpPost("updateImageAndPrediction")]
        public async Task<IActionResult> UpdateImageAndPrediction(int lesionId, string imageType, string imageDescription, string resolution, string cameraModel, string illuminationType, string predictionResult, float confidenceScore)
        {
            try
            {
                await _workflowService.UpdateImageAndPredictionAsync(User, lesionId, imageType, imageDescription, resolution, cameraModel, illuminationType, predictionResult, confidenceScore);
                return Ok("Image and Prediction updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Stage 3: Dermatologist, Doctor, and Nurse can update biopsies and diagnoses
        [HttpPost("updateBiopsyAndDiagnosis")]
        public async Task<IActionResult> UpdateBiopsyAndDiagnosis(int lesionId, string biopsyResult, string biopsyResultDescription, string diagnosisResult, string diagnosisDescription)
        {
            try
            {
                await _workflowService.UpdateBiopsyAndDiagnosisAsync(User, lesionId, biopsyResult, biopsyResultDescription, diagnosisResult, diagnosisDescription);
                return Ok("Biopsy and Diagnosis updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // Stage 4: Only Dermatologist can update final diagnoses and treatments
        [HttpPost("finalize")]
        public async Task<IActionResult> UpdateFinalDiagnosisAndTreatment(int diagnosisId, int modelPredictionId, string finalResult, string finalResultDescription, string treatmentType, bool isFollowUp)
        {
            try
            {
                await _workflowService.UpdateFinalDiagnosisAndTreatmentAsync(User, diagnosisId, modelPredictionId, finalResult, finalResultDescription, treatmentType, isFollowUp);
                return Ok("Final Diagnosis and Treatment updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
