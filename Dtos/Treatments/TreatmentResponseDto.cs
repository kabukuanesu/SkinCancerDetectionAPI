namespace SkinCancerDetectionAPI.Models
{
    public class TreatmentResponseDto
    {
        public int TreatmentId { get; set; }
        public int FinalDiagnosisId { get; set; }
        public int UserId { get; set; }
        public DateTime TreatmentDate { get; set; } = DateTime.Now;
        public string TreatmentType { get; set; } = string.Empty;
        public string TreatmentDescription { get; set; } = string.Empty;
        public bool IsFollowUp { get; set; }
        public DateTime FollowUpDate { get; set; } = DateTime.Now;
    }
}
