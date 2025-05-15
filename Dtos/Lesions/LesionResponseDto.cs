namespace SkinCancerDetectionAPI.Models
{
    public class LesionResponseDto
    {
        public int LesionId { get; set; }
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public string LesionLocation { get; set; } = string.Empty;
        public DateTime DateOfDetection { get; set; } = DateTime.Now;
        public DateTime DateOfExamination { get; set; } = DateTime.Now;
        public string LesionDescription { get; set; } = string.Empty;
        public string DermoscopicFeatures { get; set; } = string.Empty;
        public bool IsBiopsy { get; set; }
    }
}
