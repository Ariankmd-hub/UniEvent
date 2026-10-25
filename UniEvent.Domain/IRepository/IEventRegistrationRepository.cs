using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IEventRegistrationRepository
    {
        Task<List<EventRegistration>> GetAllAsync();

        Task AddAsync(EventRegistration eventRegistration);

        Task<bool> RemoveAsync(int id);

        Task<EventRegistration?> GetByIdAsync(int id); 

       // Logic
        Task<bool> IsAlreadyRegisteredAsync(int userId, int eventId);
        Task<int> GetCountByEventIdAsync(int eventId);
    }
}
