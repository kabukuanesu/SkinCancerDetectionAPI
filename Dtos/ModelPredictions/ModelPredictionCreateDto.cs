// ModelPredictionCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class ModelPredictionCreateDto
    {
        public int ImageId { get; set; }
        public string PredictionResult { get; set; } = string.Empty;
        public float ConfidenceScore { get; set; }
    }
}
