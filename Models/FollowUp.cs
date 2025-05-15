using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class FollowUp
    {
        [Key]
        public int FollowUpId { get; set; }

        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; } = null!;

        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public DateTime FollowUpDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string FollowUpResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string FollowUpDescription { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public bool IsFollowUp { get; set; } = false;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime FollowingUpDate { get; set; } = DateTime.Now;
    }
}
