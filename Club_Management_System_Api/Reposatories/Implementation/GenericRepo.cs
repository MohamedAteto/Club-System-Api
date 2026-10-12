using Club_Management_System_Api.Data;
using Club_Management_System_Api.Reposatories.Interface;
using Microsoft.EntityFrameworkCore;

namespace Club_Management_System_Api.Reposatories.Implementation
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepo(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public async Task CreateAsync(T item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            await _dbSet.AddAsync(item);


        }

        public async Task DeleteAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);
            if (item == null)
                throw new KeyNotFoundException($"Item with id {id} not found.");

            _dbSet.Remove(item);

        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            var items = await _dbSet.ToListAsync();
            
            if(items == null || items.Count == 0)
                throw new KeyNotFoundException("No items found.");

            return items;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var item = await _dbSet.FindAsync(id);

            if (item == null)
                throw new KeyNotFoundException($"Item with id {id} not found.");

            return item;
        }

        public async Task UpdateAsync(int id, T item)
        {
            var Exist = await _dbSet.FindAsync(id);

            if (Exist == null)
                throw new KeyNotFoundException($"Item with id {id} not found.");

            _context.Entry(Exist).CurrentValues.SetValues(item);


        }
    }
}
