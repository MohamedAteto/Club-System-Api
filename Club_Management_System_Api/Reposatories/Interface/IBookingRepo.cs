using Club_Management_System_Api.Models;

namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IBookingRepo : IGenericRepo<Booking>
    {
        Task<ICollection<Booking>> GetBookingsFilterdWithStatusUsingPagignation(string Status , int pageNumber, int pageSize);
        Task<IEnumerable<Booking>> GetBookingsWithFacilityAsync();
        Task CancelBooking(int id);
    }
}
