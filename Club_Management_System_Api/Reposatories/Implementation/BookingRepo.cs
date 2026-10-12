using Club_Management_System_Api.Data;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class BookingRepo : GenericRepo<Booking>, IBookingRepo
    {
        private readonly AppDbContext _context;

        public BookingRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

      

        public async Task<ICollection<Booking>> GetBookingsFilterdWithStatusUsingPagignation(string Status, int pageNumber, int pageSize)
        {
            var items = await _context.Bookings.Where(b => b.Status == Status)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            return items;

        }

        public async Task<IEnumerable<Booking>> GetBookingsWithFacilityAsync()
        {
            return await _context.Bookings
                .Include(b => b.Facility)
                .ToListAsync();
        }

        public async Task CancelBooking(int id)
        {
            var item = await _context.Bookings.FindAsync(id);
            if(item == null)
                throw new KeyNotFoundException($"Booking with id {id} not found.");

            item.Status = "Cancelled";
             _context.Bookings.Update(item);
            await _context.SaveChangesAsync();
            
        }

    }
}
