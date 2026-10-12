using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Club_Management_System_Api.Models
{
    public class Booking
    {
        

        [Key]
        public int BoookingId { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [Required , Range(1,100)]
        public int GroupSize { get; set; }
        [Required]
        public string Status { get; set; }
        

        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }


        public int FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; }

    }
}
