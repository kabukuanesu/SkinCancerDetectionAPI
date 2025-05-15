// BiopsyUpdateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class BiopsyUpdateDto
    {
        public int UserId { get; set; }
        public string BiopsyResult { get; set; } = string.Empty;
        public string BiopsyResultDescription { get; set; } = string.Empty;
    }
}
