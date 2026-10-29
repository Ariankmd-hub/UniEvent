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


        public async Task AddAsync(User user, CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user);
            
        }

        

        public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<User> allUsers = await _context.Users.AsNoTracking().ToListAsync(cancellationToken);

            return allUsers;
        }

        public async Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            User? user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
            return user;

        }

        public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken)
        {
            // if there is = > true
            return await _context.Users.AnyAsync(x=>x.EmailAddress == email, cancellationToken);
        }

        public async Task<bool> IsUniversityIdExistsAsync(string universityIdNumber, CancellationToken cancellationToken)
        {
            return await _context.Users.AnyAsync(x=>x.UniversityIdNumber == universityIdNumber, cancellationToken);
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken)
        {
            User? user =await _context.Users.FindAsync(id);
            if (user == null) { return false; }
            _context.Users.Remove(user);
           
            return true; 
           

        }
    }
}
