using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class FinalDiagnosis
    {
        [Key]
        public int FinalDiagnosisId { get; set; }

        public int DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; } = null!;

        public int UserId { get; set; }

        public int ModelPredictionId { get; set; }
        public ModelPrediction ModelPrediction { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string FinalResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string FinalResultDescription { get; set; } = "";
    }
}
