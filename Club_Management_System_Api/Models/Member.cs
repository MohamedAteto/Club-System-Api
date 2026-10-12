using System.ComponentModel.DataAnnotations;

namespace Club_Management_System_Api.Models
{
    public class Member
    {

        [Key]
        public int MemberId { get; set; }
        [Required , StringLength(100,MinimumLength =3)]
        public string MemberFullName { get; set; }
        [Required, EmailAddress]
        public string MemberEmail { get; set; }
        [Required,Phone]
        public string MemberPhone { get; set; }
        [Required]
        public DateTime MemberBirthDate { get; set; }

        public MemberShip? MemberShip { get; set; }


        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
