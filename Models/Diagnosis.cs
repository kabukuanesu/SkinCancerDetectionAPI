using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisId { get; set; }

        [ForeignKey("Biopsy")]
        public int BiopsyId { get; set; }
        public Biopsy Biopsy { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DiagnosisDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string DiagnosisResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string DiagnosisDescription { get; set; } = "";

        public ICollection<FinalDiagnosis> FinalDiagnoses { get; set; } = new List<FinalDiagnosis>(); // navigation property
    }
}
