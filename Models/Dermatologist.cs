using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Dermatologist
    {
        [Key]
        public int DermatologistId { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName="nvarchar(100)")]
        public string FirstName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateOnly DateOfBirth { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string Gender { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string PhysicalAddress { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string EmailAddress { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string PhoneNumber { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(100)")]
        public DateTime DateUpdated { get; set; } = DateTime.Now;

        public ICollection<Authentication> Authentications { get; set; } = new List<Authentication>(); // navigation property

        public ICollection<Patient> Patients { get; set; } = new List<Patient>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
