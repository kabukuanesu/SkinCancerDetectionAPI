using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Authentication
    {
        [Key]
        public int AuthenticationId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; } = null!; // navigation property with null-forgiving operator

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
