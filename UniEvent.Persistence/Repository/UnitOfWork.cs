using UniEvent.Domain.IRepository;

namespace UniEvent.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniEventDbContext _context;

        public IEventRepository EventRepository { get; }
        public IUserRepository UserRepository { get; }
        public IEventRegistrationRepository EventRegistrationRepository { get; }

        public UnitOfWork(UniEventDbContext context, IEventRepository eventRepository,
            IUserRepository userRepository, IEventRegistrationRepository eventRegistrationRepository) 
        {
            _context = context;
            EventRepository = eventRepository;
            UserRepository = userRepository;
            EventRegistrationRepository = eventRegistrationRepository;

        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
