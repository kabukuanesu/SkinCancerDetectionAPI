namespace SkinCancerDetectionAPI.Models
{
    public class DiagnosisResponseDto
    {
        public int DiagnosisId { get; set; }
        public int BiopsyId { get; set; }
        public DateTime DiagnosisDate { get; set; } = DateTime.Now;
        public string DiagnosisResult { get; set; } = string.Empty;
        public string DiagnosisDescription { get; set; } = string.Empty;
    }
}
