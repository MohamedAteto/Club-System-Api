

using Club_Management_System_Api.Models;

namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IMemberRepo : IGenericRepo<Member>
    {
        public Task<ICollection<Member>> GetMemberWithPagignation(int pageNumber, int pageSize);
        public Task<ICollection<Member>> GetMemberByName(string name);
        //public Task<ICollection<Member>> GetActiveMembers();


    }
}
