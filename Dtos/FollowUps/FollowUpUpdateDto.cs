// FollowUpUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class FollowUpUpdateDto
    {
        public int UserId { get; set; }
        public string FollowUpResult { get; set; } = string.Empty;
        public string FollowUpDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
    }
}
