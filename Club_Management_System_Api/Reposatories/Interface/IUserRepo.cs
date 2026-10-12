using Club_Management_System_Api.Models;

namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<User?> GetUserByUserName(string name);
    }
}
