// ModelPredictionUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class ModelPredictionUpdateDto
    {
        public int UserId { get; set; }
        public string PredictionResult { get; set; } = string.Empty;
        public float ConfidenceScore { get; set; }
    }
}
