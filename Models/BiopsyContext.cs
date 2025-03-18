using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class BiopsyContext : DbContext
    {
        public BiopsyContext(DbContextOptions<BiopsyContext> options) : base(options)
        {
        }

        public DbSet<Biopsy> Biopsies { get; set; }
    }
}
