namespace SkinCancerDetectionAPI.Models
{
    public class PatientResponseDto
    {
        public int PatientId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string PhysicalAddress { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string MedicalHistory { get; set; } = string.Empty;
        public string FamilyHistory { get; set; } = string.Empty;
        public string SkinType { get; set; } = string.Empty;
        public bool IsScheduled { get; set; }
        public DateTime Scheduled { get; set; } = DateTime.Now;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime DateUpdated { get; set; } = DateTime.Now;
    }
}
