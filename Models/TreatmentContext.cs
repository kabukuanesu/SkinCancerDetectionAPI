using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class TreatmentContext : DbContext
    {
        public TreatmentContext(DbContextOptions<TreatmentContext> options) : base(options)
        {
        }

        public DbSet<Treatment> Treatments { get; set; }
    }
}
