// DiagnosisCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class DiagnosisCreateDto
    {
        public int BiopsyId { get; set; }
        public string DiagnosisResult { get; set; } = string.Empty;
        public string DiagnosisDescription { get; set; } = string.Empty;
    }
}
