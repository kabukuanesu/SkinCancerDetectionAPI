using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class DermatologistContext : DbContext
    {
        public DermatologistContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Dermatologist> Dermatologists { get; set; }
    }
}
