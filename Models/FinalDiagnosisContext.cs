using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class FinalDiagnosisContext : DbContext
    {
        public FinalDiagnosisContext(DbContextOptions<FinalDiagnosisContext> options) : base(options)
        {
        }

        public DbSet<FinalDiagnosis> FinalDiagnoses { get; set; }
    }
}
