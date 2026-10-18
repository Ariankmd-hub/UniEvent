using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task AddAsync(User user);

        Task<bool> RemoveAsync(User user);

        Task<User?> GetByIdAsync(int id);
    }
}
