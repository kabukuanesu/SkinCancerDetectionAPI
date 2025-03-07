using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class ModelPredictionContext : DbContext
    {
        public ModelPredictionContext(DbContextOptions<ModelPredictionContext> options) : base(options)
        {
        }

        public DbSet<ModelPrediction> ModelPredictions { get; set; }
    }
}
