// ModelPredictionUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class ModelPredictionUpdateDto
    {
        public string PredictionResult { get; set; } = string.Empty;
        public float ConfidenceScore { get; set; }
    }
}
