// LesionCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class LesionCreateDto
    {
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public string LesionLocation { get; set; } = string.Empty;
        public string LesionDescription { get; set; } = string.Empty;
        public string DermoscopicFeatures { get; set; } = string.Empty;
        public bool IsBiopsy { get; set; }
    }
}
