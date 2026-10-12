namespace Club_Management_System_Api.DTOs.BookingDTOs
{
    public class CreateBookingDTO
    {
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public int GroupSize { get; set; }
        public string Status { get; set; }
        public int MemberId { get; set; }
        public int FacilityId { get; set; }
    }
}
