namespace Club_Management_System_Api.DTOs.MemberShipDTOs
{
    public class CreateMemberShipDTO
    {
        public string MemberShipType { get; set; }
        public DateTime MemberShipStartDate { get; set; }
        public DateTime MemberShipEndDate { get; set; }
        public decimal MemberShipPrice { get; set; }
        public string MemberShipStatus { get; set; }
        public int MemberId { get; set; }
    }
}
