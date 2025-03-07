using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class FollowUpContext : DbContext
    {
        public FollowUpContext(DbContextOptions<FollowUpContext> options) : base(options)
        {
        }

        public DbSet<FollowUp> FollowUps { get; set; }
    }
}
