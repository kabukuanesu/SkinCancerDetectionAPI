using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Lesion
    {
        [Key]
        public int LesionId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public string LesionLocation { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateOfDetection { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateOfExamination { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string LesionDescription { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string DermoscopicFeatures { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public bool IsBiopsy { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public DateTime BiopsyDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string BiopsyResult { get; set; } = "";

        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>(); // navigation property

        public ICollection<Image> Images { get; set; } = new List<Image>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
