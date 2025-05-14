namespace SkinCancerDetectionAPI.Dtos.ManageWorkflow
{
    public class ManageWorkflowUpdateDto
    {
        public string Status { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
