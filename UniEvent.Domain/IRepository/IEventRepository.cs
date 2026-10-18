using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync();

        Task AddAsync(Event eventEntity);

        Task<bool> RemoveAsync(Event eventEntity);

        Task<Event?> GetByIdAsync(int id);

       
      }
}
