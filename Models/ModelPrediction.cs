using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class ModelPrediction
    {
        [Key]
        public int ModelPredictionId { get; set; }

        public int ImageId { get; set; }
        public Image Image { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string PredictionResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public float ConfidenceScore { get; set; }
    }
}
