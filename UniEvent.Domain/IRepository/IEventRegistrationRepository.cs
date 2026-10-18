using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IEventRegistrationRepository
    {
        Task<List<EventRegistration>> GetAllAsync();

        Task AddAsync(EventRegistration eventRegistration);

        Task<bool> RemoveAsync(EventRegistration eventRegistration);

        Task<EventRegistration?> GetByIdAsync(int id); 
    }
}
