using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkinCancerDetectionAPI.Data;
using SkinCancerDetectionAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkinCancerDetectionAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("counts")]
        public async Task<IActionResult> GetDashboardCounts()
        {
            var today = DateTime.Today;

            // 1. Total number of rows with 'scheduled' date later than today's date in Patients Table
            var scheduledPatientsCount = await _context.Patients
                .Where(p => p.Scheduled > today)
                .CountAsync();

            // 2. Total number of rows in ApplicationUsers Table
            var usersCount = await _context.ApplicationUsers.CountAsync();

            // 3. Total number of rows in ModelPredictions Table
            var modelPredictionsCount = await _context.ModelPredictions.CountAsync();

            // 4. Total number of rows with 'followingUpDate' later than today's date in FollowUpsTable
            var upcomingFollowUpsCount = await _context.FollowUps
                .Where(f => f.FollowingUpDate > today)
                .CountAsync();

            return Ok(new
            {
                ScheduledPatientsCount = scheduledPatientsCount,
                UsersCount = usersCount,
                ModelPredictionsCount = modelPredictionsCount,
                UpcomingFollowUpsCount = upcomingFollowUpsCount
            });
        }

        [HttpGet("upcoming-events")]
        public async Task<IActionResult> GetUpcomingEvents()
        {
            var today = DateTime.Today;

            // Get scheduled patients (condition 1)
            var scheduledPatients = await _context.Patients
                .Where(p => p.Scheduled > today)
                .Select(p => new
                {
                    Type = "Appointment",
                    Date = p.Scheduled,
                    Id = p.PatientId,
                    Description = $"Patient: {p.FirstName} {p.LastName}"
                })
                .ToListAsync();

            // Get follow-ups (condition 4)
            var followUps = await _context.FollowUps
                .Where(f => f.FollowingUpDate > today)
                .Select(f => new
                {
                    Type = "FollowUp",
                    Date = f.FollowingUpDate,
                    Id = f.FollowUpId,
                    Description = $"Follow-up for treatment {f.TreatmentId}"
                })
                .ToListAsync();

            // Combine and sort by date
            var upcomingEvents = scheduledPatients
                .Concat(followUps)
                .OrderBy(e => e.Date)
                .ToList();

            return Ok(upcomingEvents);
        }
    }
}
