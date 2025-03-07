using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string DateOfBirth { get; set; } = "";

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

        public ICollection<Dermatologist> Dermatologists { get; set; } = new List<Dermatologist>(); // navigation property

        public ICollection<AdminAuthentication> AdminAuthentications { get; set; } = new List<AdminAuthentication>(); // navigation property

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
