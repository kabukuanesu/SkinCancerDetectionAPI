using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Services;

namespace SkinCancerDetectionAPI.Controllers
{
    [ApiController]
    [Route("api/export")]
    [Authorize]
    public class ExportController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IExportService _exportService;

        public ExportController(AppDbContext context, IExportService exportService)
        {
            _context = context;
            _exportService = exportService;
        }

        [HttpGet("patients/pdf")]
        public async Task<IActionResult> ExportPatientsPdf()
        {
            var patients = await _context.Patients.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(patients, "Patients Report");
            return File(pdfBytes, "application/pdf", "patients_report.pdf");
        }

        [HttpGet("patients/csv")]
        public async Task<IActionResult> ExportPatientsCsv()
        {
            var patients = await _context.Patients.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(patients);
            return File(csvBytes, "text/csv", "patients_data.csv");
        }

        [HttpGet("bookings/pdf")]
        public async Task<IActionResult> ExportBookingsPdf()
        {
            var bookings = await _context.Bookings.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(bookings, "Bookings Report");
            return File(pdfBytes, "application/pdf", "bookings_report.pdf");
        }

        [HttpGet("bookings/csv")]
        public async Task<IActionResult> ExportBookingsCsv()
        {
            var bookings = await _context.Bookings.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(bookings);
            return File(csvBytes, "text/csv", "bookings_data.csv");
        }

        [HttpGet("lesions/pdf")]
        public async Task<IActionResult> ExportLesionsPdf()
        {
            var lesions = await _context.Lesions.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(lesions, "Lesions Report");
            return File(pdfBytes, "application/pdf", "lesions_report.pdf");
        }

        [HttpGet("lesions/csv")]
        public async Task<IActionResult> ExportLesionsCsv()
        {
            var lesions = await _context.Lesions.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(lesions);
            return File(csvBytes, "text/csv", "lesions_data.csv");
        }

        [HttpGet("workflows/pdf")]
        public async Task<IActionResult> ExportWorkflowsPdf()
        {
            var workflows = await _context.Workflows.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(workflows, "Workflows Report");
            return File(pdfBytes, "application/pdf", "workflows_report.pdf");
        }

        [HttpGet("workflows/csv")]
        public async Task<IActionResult> ExportWorkflowsCsv()
        {
            var workflows = await _context.Workflows.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(workflows);
            return File(csvBytes, "text/csv", "workflows_data.csv");
        }

        [HttpGet("biopsies/pdf")]
        public async Task<IActionResult> ExportBiopsiesPdf()
        {
            var biopsies = await _context.Biopsies.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(biopsies, "Biopsies Report");
            return File(pdfBytes, "application/pdf", "biopsies_report.pdf");
        }

        [HttpGet("biopsies/csv")]
        public async Task<IActionResult> ExportBiopsiesCsv()
        {
            var biopsies = await _context.Biopsies.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(biopsies);
            return File(csvBytes, "text/csv", "biopsies_data.csv");
        }

        [HttpGet("images/pdf")]
        public async Task<IActionResult> ExportImagesPdf()
        {
            var images = await _context.Images.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(images, "Images Report");
            return File(pdfBytes, "application/pdf", "images_report.pdf");
        }

        [HttpGet("images/csv")]
        public async Task<IActionResult> ExportImagesCsv()
        {
            var images = await _context.Images.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(images);
            return File(csvBytes, "text/csv", "images_data.csv");
        }

        [HttpGet("diagnoses/pdf")]
        public async Task<IActionResult> ExportDiagnosesPdf()
        {
            var diagnoses = await _context.Diagnoses.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(diagnoses, "Diagnoses Report");
            return File(pdfBytes, "application/pdf", "diagnoses_report.pdf");
        }

        [HttpGet("diagnoses/csv")]
        public async Task<IActionResult> ExportDiagnosesCsv()
        {
            var diagnoses = await _context.Diagnoses.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(diagnoses);
            return File(csvBytes, "text/csv", "diagnoses_data.csv");
        }

        [HttpGet("treatments/pdf")]
        public async Task<IActionResult> ExportTreatmentsPdf()
        {
            var treatments = await _context.Treatments.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(treatments, "Treatments Report");
            return File(pdfBytes, "application/pdf", "treatments_report.pdf");
        }

        [HttpGet("treatments/csv")]
        public async Task<IActionResult> ExportTreatmentsCsv()
        {
            var treatments = await _context.Treatments.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(treatments);
            return File(csvBytes, "text/csv", "treatments_data.csv");
        }

        [HttpGet("followups/pdf")]
        public async Task<IActionResult> ExportFollowUpsPdf()
        {
            var followUps = await _context.FollowUps.ToListAsync();
            var pdfBytes = _exportService.GeneratePdf(followUps, "Follow-ups Report");
            return File(pdfBytes, "application/pdf", "followups_report.pdf");
        }

        [HttpGet("followups/csv")]
        public async Task<IActionResult> ExportFollowUpsCsv()
        {
            var followUps = await _context.FollowUps.ToListAsync();
            var csvBytes = _exportService.GenerateCsv(followUps);
            return File(csvBytes, "text/csv", "followups_data.csv");
        }
    }
}
