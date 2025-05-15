// TreatmentUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class TreatmentUpdateDto
    {
        public int UserId { get; set; }
        public string TreatmentType { get; set; } = string.Empty;
        public string TreatmentDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
    }
}
