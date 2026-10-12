using Club_Management_System_Api.Data;
using Club_Management_System_Api.Models;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class UserRepo : GenericRepo<User>, IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByUserName(string name)
        {
            var item = await _context.Users.FirstOrDefaultAsync(s => s.UserName == name);
            return item;
        }
    }
}
