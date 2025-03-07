using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisId { get; set; }

        [ForeignKey("Lesion")]
        public int LesionId { get; set; }
        public Lesion Lesion { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DiagnosisDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string DiagnosisResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string DiagnosisDescription { get; set; } = "";

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
