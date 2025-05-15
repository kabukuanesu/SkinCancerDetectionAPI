using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkinCancerDetectionAPI.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public int UserId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public DateTime BookedDate { get; set; } = DateTime.Now;
    }
}
