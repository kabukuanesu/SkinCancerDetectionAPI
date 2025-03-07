using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class AuditLogContext : DbContext
    {
        public AuditLogContext(DbContextOptions<AuditLogContext> options) : base(options)
        {
        }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
