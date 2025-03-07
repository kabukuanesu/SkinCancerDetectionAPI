using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class DiagnosisContext : DbContext
    {
        public DiagnosisContext(DbContextOptions<DiagnosisContext> options) : base(options)
        {
        }

        public DbSet<Diagnosis> Diagnoses { get; set; }
    }
}
