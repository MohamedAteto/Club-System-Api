using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Models
{
    [Index(nameof(FacilityName), IsUnique = true)]
    public class Facility
    {
        [Key]
        public int FacilityId { get; set; }
        [Required,MaxLength(100)]
        public string FacilityName { get; set; }
        [Required, Range(1,1000)]
        public int FacilityCapacity { get; set; }
        [Required, Range(50, 10000)]
        public decimal FacilityHourlyPrice { get; set; }


        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    }
}
