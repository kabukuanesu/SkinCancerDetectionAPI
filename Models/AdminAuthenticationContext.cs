using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class AdminAuthenticationContext : DbContext
    {
        public AdminAuthenticationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AdminAuthentication> AdminAuthentications { get; set; }
    }
}
