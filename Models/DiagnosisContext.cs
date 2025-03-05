using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class DiagnosisContext : DbContext
    {
        public DiagnosisContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Diagnosis> Diagnoses { get; set; }
    }
}
