using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class FinalDiagnosis
    {
        [Key]
        public int FinalDiagnosisId { get; set; }

        [ForeignKey("Diagnosis")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("ModelPrediction")]
        public int ModelPredictionId { get; set; }
        public ModelPrediction ModelPrediction { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public string FinalResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string FinalResultDescription { get; set; } = "";

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
