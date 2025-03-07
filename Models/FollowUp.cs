using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class FollowUp
    {
        [Key]
        public int FollowUpId { get; set; }

        [ForeignKey("Treatment")]
        public int TreatmentId { get; set; }
        public Treatment Treatment { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public DateTime FollowUpDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public string FollowUpResult { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string FollowUpDescription { get; set; } = "";

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
