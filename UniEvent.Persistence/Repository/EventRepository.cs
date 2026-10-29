using Microsoft.EntityFrameworkCore;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Persistence.Repository
{
    public class EventRepository : IEventRepository
    {
        private readonly UniEventDbContext _context;
        public EventRepository(UniEventDbContext uniEventDbContext)
        {
            _context = uniEventDbContext;
        }

        public async Task AddAsync(Event eventEntity,CancellationToken cancellationToken)
        {
            await _context.Events.AddAsync(eventEntity);
            
        }   

        public async Task<List<Event>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<Event> listOfEvents = await _context.Events.AsNoTracking().ToListAsync(cancellationToken);
            return listOfEvents;
        }

        public async Task<Event?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            Event? selectedEvent = await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
            return selectedEvent;
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken)
        {
            Event? eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null) { return false; }

            _context.Events.Remove(eventEntity);
            
            int affectedRows = await _context.SaveChangesAsync(cancellationToken);
             return true; 
            
        }
    }
}
