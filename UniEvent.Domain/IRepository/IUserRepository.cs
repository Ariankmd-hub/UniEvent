using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(User user, CancellationToken cancellationToken);

        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);

        Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken);
        Task<bool> IsUniversityIdExistsAsync(string universityIdNumber, CancellationToken cancellationToken);


    }
}
