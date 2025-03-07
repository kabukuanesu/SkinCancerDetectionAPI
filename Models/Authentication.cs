using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Authentication
    {
        [Key]
        public int AuthenticationId { get; set; }

        [ForeignKey("Dermatologist")]
        public int DermatologistId { get; set; }
        public Dermatologist Dermatologist { get; set; } = null!; // navigation property with null-forgiving operator

        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string RecoveryQuestion { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string RecoveryAnswer { get; set; } = "";

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>(); // navigation property
    }
}
