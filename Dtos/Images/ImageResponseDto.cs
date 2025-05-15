namespace SkinCancerDetectionAPI.Models
{
    public class ImageResponseDto
    {
        public int ImageId { get; set; }
        public int LesionId { get; set; }
        public int UserId { get; set; }
        public string ImageType { get; set; } = string.Empty;
        public string ImageDescription { get; set; } = string.Empty;
        public string Resolution { get; set; } = string.Empty;
        public DateTime DateTaken { get; set; } = DateTime.Now;
        public string CameraModel { get; set; } = string.Empty;
        public string IlluminationType { get; set; } = string.Empty;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
