using Microsoft.EntityFrameworkCore;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        #region DbContext Ctor
        private readonly UniEventDbContext _context;
        public UserRepository(UniEventDbContext uniEventDbContext)
        {
            _context = uniEventDbContext;

        }
        #endregion


        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            List<User> allUsers = await _context.Users.AsNoTracking().ToListAsync();

            return allUsers;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            User? user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return user;

        }

        public async Task<bool> RemoveAsync(int id)
        {
            User? user =await _context.Users.FindAsync(id);
            if (user == null) { return false; }
            _context.Users.Remove(user);
            // if there was change = > it would be 1 or higher else it will be 0 
            int efectedRows = await _context.SaveChangesAsync();
            if (efectedRows > 0) 
            { return true; }
            return false;

        }
    }
}
