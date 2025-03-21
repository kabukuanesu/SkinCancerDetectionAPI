using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Lesion
    {
        [Key]
        public int LesionId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string LesionLocation { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateOfDetection { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateOfExamination { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string LesionDescription { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string DermoscopicFeatures { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public bool IsBiopsy { get; set; } = false;
    }
}
