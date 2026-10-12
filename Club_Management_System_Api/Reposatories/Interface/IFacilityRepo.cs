using Club_Management_System_Api.Models;

namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IFacilityRepo :IGenericRepo<Facility>
    {
        Task<ICollection<Facility>> GetAllFacilitiesSortedAsync();
    }
}
