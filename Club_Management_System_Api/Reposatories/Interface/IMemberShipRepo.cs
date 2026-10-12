using Club_Management_System_Api.Models;

namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IMemberShipRepo : IGenericRepo<MemberShip>
    {
        Task<ICollection<MemberShip>> GetActiveMemberShipsAsync();
        Task  Renew_MemberShipPaymentAsync(int MemberShipId);
    }
}
