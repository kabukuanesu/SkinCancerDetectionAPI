// TreatmentUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class TreatmentUpdateDto
    {
        public string TreatmentType { get; set; } = string.Empty;
        public string TreatmentDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
    }
}
