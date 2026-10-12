namespace Club_Management_System_Api.DTOs.PaymentDTOs
{
    public class PaymentStatisticsDTO
    {
        public decimal MaxAmount { get; set; }
        public decimal MinAmount { get; set; }
        public decimal TotalSum { get; set; }
        public int TotalPaymentsCount { get; set; }
    }
}
