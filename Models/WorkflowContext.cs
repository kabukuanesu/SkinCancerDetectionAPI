using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class WorkflowContext : DbContext
    {
        public WorkflowContext(DbContextOptions<WorkflowContext> options) : base(options)
        {
        }

        public DbSet<Workflow> Workflows { get; set;}
    }
}
