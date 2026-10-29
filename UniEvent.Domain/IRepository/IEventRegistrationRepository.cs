using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IEventRegistrationRepository
    {
        Task<List<EventRegistration>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(EventRegistration eventRegistration, CancellationToken cancellationToken);

        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);

        Task<EventRegistration?> GetByIdAsync(int id, CancellationToken cancellationToken); 

       // Logic
        Task<bool> IsAlreadyRegisteredAsync(int userId, int eventId, CancellationToken cancellationToken);
        Task<int> GetCountByEventIdAsync(int eventId, CancellationToken cancellationToken);
        Task<int> GetCountByUserIdAsync(int userId, CancellationToken cancellationToken);
    }
}
