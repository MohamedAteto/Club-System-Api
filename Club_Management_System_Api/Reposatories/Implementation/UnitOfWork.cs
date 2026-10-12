using Club_Management_System_Api.Data;
using Club_Management_System_Api.Reposatories.Interface;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class UnitOfWork : IunitOfWork
    {
        private readonly AppDbContext _context;
        public IMemberRepo MemberRepo { get; }

        public IMemberShipRepo MemberShipRepo { get; }

        public IFacilityRepo FacilityRepo { get; }
        public IBookingRepo BookingRepo { get; }

        public IPaymentRepo PaymentRepo { get; }

        public UnitOfWork( AppDbContext context , IMemberRepo memberRepo , IMemberShipRepo memberShipRepo 
            , IFacilityRepo FacilityRepo , IBookingRepo bookingRepo , IPaymentRepo paymentRepo) {

            _context = context;
            MemberRepo = memberRepo;
            MemberShipRepo = memberShipRepo;
            this.FacilityRepo = FacilityRepo;
            BookingRepo = bookingRepo;
            PaymentRepo = paymentRepo;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
           
        }
    }
}
