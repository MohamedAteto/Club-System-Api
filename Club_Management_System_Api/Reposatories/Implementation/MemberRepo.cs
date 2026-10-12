using Club_Management_System_Api.Data;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class MemberRepo : GenericRepo<Member>, IMemberRepo
    {
        private readonly  AppDbContext _context;
        public MemberRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<ICollection<Member>> GetActiveMembers()
        //{
        //    return await _context.Members.Where(m => m.IsActive).ToListAsync();
        //}

        public async Task<ICollection<Member>> GetMemberByName(string name)
        {
            var items = await _context.Members.Where(m => m.MemberFullName.Contains(name)).ToListAsync();

            return items;
        }

        public async Task<ICollection<Member>> GetMemberWithPagignation(int pageNumber, int pageSize)
        {
            var items = await _context.Members
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            return items;
        }
    }
}
