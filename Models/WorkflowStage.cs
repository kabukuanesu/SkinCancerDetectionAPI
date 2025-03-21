using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class WorkflowStage
    {
        [Key]
        public int WorkflowStageId { get; set; }

        public int WorkflowId { get; set; }

        public Workflow Workflow { get; set; } = null!;

        [Column(TypeName = "nvarchar(100)")]
        public string WorkflowStageName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string Status { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string AssignedTo { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime EndDate { get; set; } = DateTime.Now;
    }
}
