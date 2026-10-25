using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync();

        Task AddAsync(User user);

        Task<bool> RemoveAsync(int id);

        Task<User?> GetByIdAsync(int id);
        Task<bool> IsEmailExistsAsync(string email);
        Task<bool> IsUniversityIdExistsAsync(string universityIdNumber);


    }
}
