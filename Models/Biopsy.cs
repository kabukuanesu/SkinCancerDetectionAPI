using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Biopsy
    {
        [Key]
        public int BiopsyId { get; set; }

        [ForeignKey("Lesion")]
        public int LesionId { get; set; }
        public Lesion Lesion { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public DateTime BiopsyDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string BiopsyResult { get; set; } = "";

        public ICollection<Diagnosis> Diagnoses { get; set; } = new List<Diagnosis>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
