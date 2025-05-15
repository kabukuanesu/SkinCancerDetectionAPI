// FinalDiagnosisUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class FinalDiagnosisUpdateDto
    {
        public int UserId { get; set; }
        public string FinalResult { get; set; } = string.Empty;
        public string FinalResultDescription { get; set; } = string.Empty;
    }
}
