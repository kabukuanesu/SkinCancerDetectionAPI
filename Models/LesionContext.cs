using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class LesionContext : DbContext
    {
        public LesionContext(DbContextOptions<LesionContext> options) : base(options)
        {
        }

        public DbSet<Lesion>Lesions { get; set; }
    }
}
