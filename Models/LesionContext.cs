using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class LesionContext : DbContext
    {
        public LesionContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Lesion>Lesions { get; set; }
    }
}
