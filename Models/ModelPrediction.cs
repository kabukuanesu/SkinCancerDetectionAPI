using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class ModelPrediction
    {
        [Key]
        public int ModelPredictionId { get; set; }

        [ForeignKey("Image")]
        public int ImageId { get; set; }
        public Image Image { get; set; } = null!; // navigation property with null forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public string PredictionResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public float ConfidenceScore { get; set; }

        public ICollection<Treatment> Treatments { get; set; } = new List<Treatment>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
