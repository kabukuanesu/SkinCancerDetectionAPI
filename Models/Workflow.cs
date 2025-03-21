using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Workflow
    {
        [Key]
        public int WorkflowId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string Status { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string CreatedBy { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
