namespace Club_Management_System_Api.DTOs.BookingDTOs
{
    public class FacilityBookingReportDTO
    {
        public string FacilityName { get; set; } = string.Empty;
        public int TotalBookings { get; set; }
        public int TotalGroupSize { get; set; }
    }
}
