using UniEvent.Domain.Entities;

namespace UniEvent.Domain.IRepository
{
    public interface IEventRepository
    {
        Task<List<Event>> GetAllAsync(CancellationToken cancellationToken);

        Task AddAsync(Event eventEntity, CancellationToken cancellationToken);

        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);

        Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken);

       
      }
}
