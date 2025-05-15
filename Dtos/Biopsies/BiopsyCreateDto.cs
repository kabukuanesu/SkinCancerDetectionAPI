// BiopsyCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class BiopsyCreateDto
    {
        public int LesionId { get; set; }
        public int UserId { get; set; }
        public string BiopsyResult { get; set; } = string.Empty;
        public string BiopsyResultDescription { get; set; } = string.Empty;
    }
}
