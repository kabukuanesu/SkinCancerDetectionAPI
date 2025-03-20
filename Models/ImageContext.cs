using Microsoft.EntityFrameworkCore;

namespace SkinCancerDetectionAPI.Models
{
    public class ImageContext : DbContext
    {
        public ImageContext(DbContextOptions<ImageContext> options) : base(options)
        {
        }

        public DbSet<Image> Images { get; set; }
    }

}
