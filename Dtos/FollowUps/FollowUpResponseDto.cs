namespace SkinCancerDetectionAPI.Models
{
    public class FollowUpResponseDto
    {
        public int FollowUpId { get; set; }
        public int TreatmentId { get; set; }
        public int UserId { get; set; }
        public DateTime FollowUpDate { get; set; } = DateTime.Now;
        public string FollowUpResult { get; set; } = string.Empty;
        public string FollowUpDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
        public DateTime FollowingUpDate { get; set; } = DateTime.Now;
    }
}
