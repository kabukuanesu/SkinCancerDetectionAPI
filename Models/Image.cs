using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }

        public int LesionId { get; set; }
        public Lesion Lesion { get; set; } = null!;

        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string ImageType { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string ImageDescription { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string Resolution { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateTaken { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string CameraModel { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string IlluminationType { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
