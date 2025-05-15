// TreatmentCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class TreatmentCreateDto
    {
        public int UserId { get; set; }
        public int FinalDiagnosisId { get; set; }
        public string TreatmentType { get; set; } = string.Empty;
        public string TreatmentDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
    }
}
