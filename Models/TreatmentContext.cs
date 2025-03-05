using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class TreatmentContext : DbContext
    {
        public TreatmentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Treatment> Treatments { get; set; }
    }
}
