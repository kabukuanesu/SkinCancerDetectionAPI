using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class AdminAuthentication
    {
        [Key]
        public int AdminAuthenticationId { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; } = null!; // navigation property

        [Column(TypeName = "nvarchar(100)")]
        public string Username { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string Password { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string RecoveryQuestion { get; set; } = "";

        [Column(TypeName = "nvarchar(100)")]
        public string RecoveryAnswer { get; set; } = "";
    }
}
