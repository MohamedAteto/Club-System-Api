using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Club_Management_System_Api.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        [Required, Range(100, 10000)]
        public decimal PaymentAmount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }
     
        [Required]
        public string PaymentMethod { get; set; }


        public int MemberShipId { get; set; }
        [ForeignKey("MemberShipId")]
        public MemberShip MemberShip { get; set; }
    }
}
