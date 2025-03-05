using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class FollowUpContext : DbContext
    {
        public FollowUpContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FollowUp> FollowUps { get; set; }
    }
}
