using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }

        [ForeignKey("Dermatologist")]
        public int DermatologistId { get; set; }
        public Dermatologist Dermatologist { get; set; } = null!; // navigation property

        [Column(TypeName ="nvarchar(100)")]
        public string FirstName { get; set; } = "";

        [Column(TypeName ="nvarchar(100)")]
        public string LastName { get; set; } = "";

        [Column(TypeName ="nvarchar(100)")]
        public string DateOfBirth { get; set; } = "";

        [Column(TypeName ="nvarchar(100)")]
        public string Gender { get; set; } = "";

        [Column(TypeName ="nvarchar(100)")]
        public string PhysicalAddress { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string EmailAddress { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string PhoneNumber { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string MedicalHistory { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string FamilyHistory { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string SkinType { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public ICollection<Lesion> Lesions { get; set; } = new List<Lesion>(); // navigation property
    }
}
