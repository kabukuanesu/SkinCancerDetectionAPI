using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        [ForeignKey("Diagnosis")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("ModelPrediction")]
        public int ModelPredictionId { get; set; }
        public ModelPrediction ModelPrediction { get; set; } = null!; // navigation property

        [Column(TypeName = "nvarchar(100)")]
        public DateTime TreatmentDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string TreatmentType { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string TreatmentDescription { get; set; } = "";

        public ICollection<FollowUp> FollowUps { get; set; } = new List<FollowUp>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
