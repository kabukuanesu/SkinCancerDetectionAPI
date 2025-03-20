using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Workflow
    {
        [Key]
        public int WorkflowId { get; set; }

        [ForeignKey("Patient")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public string Status { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string CreatedBy { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public ICollection<WorkflowStage> WorkflowStages { get; set; } = new List<WorkflowStage>(); // navigation property
    }
}
