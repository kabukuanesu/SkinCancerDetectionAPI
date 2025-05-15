// DiagnosisUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class DiagnosisUpdateDto
    {
        public int UserId { get; set; }
        public string DiagnosisResult { get; set; } = string.Empty;
        public string DiagnosisDescription { get; set; } = string.Empty;
    }
}
