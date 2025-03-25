// FinalDiagnosisCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class FinalDiagnosisCreateDto
    {
        public int DiagnosisId { get; set; }
        public int ModelPredictionId { get; set; }
        public string FinalResult { get; set; } = string.Empty;
        public string FinalResultDescription { get; set; } = string.Empty;
    }
}
