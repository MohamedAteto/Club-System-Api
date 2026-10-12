using Club_Management_System_Api.Data;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class FacilityRepo : GenericRepo<Facility>, IFacilityRepo
    {
        private readonly AppDbContext _context;
        public FacilityRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<ICollection<Facility>> GetAllFacilitiesSortedAsync()
        {
            var items = await _context.Facilities
                .OrderBy(f => f.FacilityName)
                .ToListAsync();

            return items;
        }
    }
}
