namespace Club_Management_System_Api.Reposatories.Interface
{
    public interface IGenericRepo<T> where T : class
    {
        Task CreateAsync(T item);
        Task DeleteAsync(int id);
        Task<T> GetByIdAsync(int id);
        Task<ICollection<T>> GetAllAsync();
        Task UpdateAsync(int id,T item);

    }
}
