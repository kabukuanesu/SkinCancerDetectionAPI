using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class AdminAuthenticationContext : DbContext
    {
        public AdminAuthenticationContext(DbContextOptions<AdminAuthenticationContext> options) : base(options)
        {
        }

        public DbSet<AdminAuthentication> AdminAuthentications { get; set; }
    }
}
