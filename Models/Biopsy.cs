using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Biopsy
    {
        [Key]
        public int BiopsyId { get; set; }

        public int LesionId { get; set; }
        public Lesion Lesion { get; set; } = null!;

        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public DateTime BiopsyDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string BiopsyResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string BiopsyResultDescription { get; set; } = "";
    }
}
