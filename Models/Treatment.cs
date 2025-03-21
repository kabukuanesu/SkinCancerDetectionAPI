using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Treatment
    {
        [Key]
        public int TreatmentId { get; set; }

        public int FinalDiagnosisId { get; set; }
        public FinalDiagnosis FinalDiagnosis { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime TreatmentDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string TreatmentType { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string TreatmentDescription { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public bool IsFollowUp { get; set; } = false;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime FollowUpDate { get; set; } = DateTime.Now;
    }
}
