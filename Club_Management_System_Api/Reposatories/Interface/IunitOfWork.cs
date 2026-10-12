namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IunitOfWork
    {
         IMemberRepo MemberRepo { get; }
        IMemberShipRepo MemberShipRepo { get; }
        IFacilityRepo FacilityRepo { get; }
        IBookingRepo BookingRepo { get; }
        IPaymentRepo PaymentRepo { get; }

        Task SaveChangesAsync();
    }
}
