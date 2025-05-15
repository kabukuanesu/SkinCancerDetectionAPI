namespace SkinCancerDetectionAPI.Models
{
    public class BiopsyResponseDto
    {
        public int BiopsyId { get; set; }
        public int LesionId { get; set; }
        public int UserId { get; set; }
        public DateTime BiopsyDate { get; set; } = DateTime.Now;
        public string BiopsyResult { get; set; } = string.Empty;
        public string BiopsyResultDescription { get; set; } = string.Empty;
    }
}
