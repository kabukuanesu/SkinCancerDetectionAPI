namespace SkinCancerDetectionAPI.Models
{
    public class ModelPredictionResponseDto
    {
        public int ModelPredictionId { get; set; }
        public int ImageId { get; set; }
        public int UserId { get; set; }
        public string PredictionResult { get; set; } = string.Empty;
        public float ConfidenceScore { get; set; }
    }
}
