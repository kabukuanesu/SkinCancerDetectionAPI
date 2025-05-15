namespace SkinCancerDetectionAPI.Models
{
    public class FinalDiagnosisResponseDto
    {
        public int FinalDiagnosisId { get; set; }
        public int DiagnosisId { get; set; }
        public int ModelPredictionId { get; set; }
        public int UserId { get; set; }
        public string FinalResult { get; set; } = string.Empty;
        public string FinalResultDescription { get; set; } = string.Empty;
    }
}
