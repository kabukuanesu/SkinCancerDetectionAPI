// ImageCreateDto.cs
namespace SkinCancerDetectionAPI.Models
{
    public class ImageCreateDto
    {
        public int LesionId { get; set; }
        public string ImageType { get; set; } = string.Empty;
        public string ImageDescription { get; set; } = string.Empty;
        public string Resolution { get; set; } = string.Empty;
        public string CameraModel { get; set; } = string.Empty;
        public string IlluminationType { get; set; } = string.Empty;
    }
}
