// FollowUpUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class FollowUpUpdateDto
    {
        public string FollowUpResult { get; set; } = string.Empty;
        public string FollowUpDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
    }
}
