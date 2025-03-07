using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("AdminAuthentication")]
        public int AdminAuthenticationId { get; set; }
        public AdminAuthentication AdminAuthentication { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("Dermatologist")]
        public int DermatologistId { get; set; }
        public Dermatologist Dermatologist { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("Authentication")]
        public int AuthenticationId { get; set; }
        public Authentication Authentication { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("Lesion")]
        public int LesionId { get; set; }
        public Lesion Lesion { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("Diagnosis")]
        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; } = null!; // navigation property with null-forving operator

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image Image { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("ModelPrediction")]
        public int ModelPredictionId { get; set; }
        public ModelPrediction ModelPrediction { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("Treatment")]
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; } = null!; // navigation property with null-forgiving operator

        [ForeignKey("FollowUp")]
        public int FollowUpId { get; set; }
        public FollowUp FollowUp { get; set; } = null!; // navigation property with null-forgiving operator
    }
}
