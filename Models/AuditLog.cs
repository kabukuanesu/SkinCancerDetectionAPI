using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("Lesion")]
        public int LesionId { get; set; }
        public Lesion Lesion { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("Biopsy")]
        public int BiopsyId { get; set; }
        public Biopsy Biopsy { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("Diagnosis")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image Image { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("ModelPrediction")]
        public int ModelPredictionId { get; set; }
        public ModelPrediction ModelPrediction { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("FinalDiagnosis")]
        public int FinalDiagnosisId { get; set; }
        public FinalDiagnosis FinalDiagnosis { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("Treatment")]
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; } = null!; // navigation property with a null-forgiving operator

        [ForeignKey("FollowUp")]
        public int FollowUpId { get; set; }
        public FollowUp FollowUp { get; set; } = null!; // navigation property with a null-forgiving operator
    }
}
