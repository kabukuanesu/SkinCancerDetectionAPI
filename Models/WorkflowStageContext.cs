using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class WorkflowStageContext : DbContext
    {
        public WorkflowStageContext(DbContextOptions<WorkflowStageContext> options) : base(options)
        {
        }

        public DbSet<WorkflowStage> WorkflowStages { get; set; }
    }
}
