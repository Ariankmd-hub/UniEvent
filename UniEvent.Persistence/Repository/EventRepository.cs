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

        public async Task AddAsync(Event eventEntity)
        {
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Event>> GetAllAsync()
        {
            List<Event> listOfEvents = await _context.Events.AsNoTracking().ToListAsync();
            return listOfEvents;
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            Event? selectedEvent = await _context.Events.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            return selectedEvent;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            Event? eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null) { return false; }

            _context.Events.Remove(eventEntity);
            
            int affectedRows = await _context.SaveChangesAsync();
            if (affectedRows > 0) { return true; }
            return false;
        }
    }
}
