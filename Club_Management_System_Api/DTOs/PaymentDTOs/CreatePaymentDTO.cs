using System.ComponentModel.DataAnnotations;

namespace Club_Management_System_Api.DTOs.PaymentDTOs
{
    public class CreatePaymentDTO
    {
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public int MemberShipId { get; set; }
    }
}
