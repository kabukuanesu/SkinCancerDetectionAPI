namespace SkinCancerDetectionAPI.Dtos.ManageWorkflow
{
    public class ManageWorkflowResponseDto
    {
        public int WorkflowId { get; set; }
        public int PatientId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
