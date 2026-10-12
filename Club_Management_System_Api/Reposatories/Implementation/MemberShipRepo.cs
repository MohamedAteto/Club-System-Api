using Club_Management_System_Api.Data;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class MemberShipRepo : GenericRepo<MemberShip>, IMemberShipRepo
    {
        private readonly AppDbContext _context;
        public MemberShipRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<ICollection<MemberShip>> GetActiveMemberShipsAsync()
        {
            return await _context.MemberShips.Where(m => m.MemberShipStatus == "Active").ToListAsync();
        }

        public async Task Renew_MemberShipPaymentAsync(int MemberShipId)
        {
            var item = await _context.MemberShips.FindAsync(MemberShipId);
            if (item is null)
                throw new ArgumentException("Invalid MemberShipId");

          
             if(item.MemberShipType == "Basic")
                item.MemberShipEndDate = DateTime.Now.AddMonths(1);

            else if (item.MemberShipType == "Silver")
                item.MemberShipEndDate = DateTime.Now.AddMonths(3);

           else if (item.MemberShipType == "Gold")
                item.MemberShipEndDate = DateTime.Now.AddYears(1);

             else
                throw new ArgumentException("Invalid MemberShipType");

            _context.SaveChanges();
        }
    }
}
