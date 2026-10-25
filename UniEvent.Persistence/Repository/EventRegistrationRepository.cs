using Microsoft.EntityFrameworkCore;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Persistence.Repository
{
    public class EventRegistrationRepository : IEventRegistrationRepository
    {

        #region DbContext Ctor
        private readonly UniEventDbContext _context;
        public EventRegistrationRepository(UniEventDbContext uniEventDbContext)
        {
            _context = uniEventDbContext;

        }
        #endregion
        public async Task AddAsync(EventRegistration eventRegistration)
        {
            await _context.EventRegistrations.AddAsync(eventRegistration);
            await _context.SaveChangesAsync();
        }

        public async Task<List<EventRegistration>> GetAllAsync()
        {
            List<EventRegistration> eventRegistrations = 
                await _context.EventRegistrations.AsNoTracking().ToListAsync();
            return eventRegistrations;
        }

        public async Task<EventRegistration?> GetByIdAsync(int id)
        {
           EventRegistration? eventRegistration = await _context.EventRegistrations.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
            return eventRegistration;
        }

        public async Task<int> GetCountByEventIdAsync(int eventId)
        {
            return await _context.EventRegistrations.CountAsync(e => e.EventId == eventId);
        }

        public async Task<bool> IsAlreadyRegisteredAsync(int userId, int eventId)
        {
            
            return await _context.EventRegistrations
                   .AnyAsync(r => r.UserId == userId && r.EventId == eventId);
        }

        public async Task<bool> RemoveAsync(int id)
        {
            EventRegistration? eventRegistration =await _context.EventRegistrations.FindAsync(id);
            if(eventRegistration == null) {return false;}
            
            _context.EventRegistrations.Remove(eventRegistration);
            int efectedRows = await _context.SaveChangesAsync();
            if (efectedRows > 0)
            { return true; }
            return false;
        }
    }
}
