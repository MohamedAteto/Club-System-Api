using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club_Management_System_Api.Models
{
    public class MemberShip
    {
        [Key]
        public int MemberShipId { get; set; }
        [Required]
        public string MemberShipType { get; set; }
        [Required]
        public DateTime MemberShipStartDate { get; set; }
        [Required]
        public DateTime MemberShipEndDate { get; set; }
        [Required , Range(100,10000)]

        public decimal MemberShipPrice { get; set; }
        [Required]

        public string MemberShipStatus { get; set; }


        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public Member Member { get; set; }

        public ICollection<Payment> payments { get; set; } = new List<Payment>();

    }
}
