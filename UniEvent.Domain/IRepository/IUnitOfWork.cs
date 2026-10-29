namespace UniEvent.Domain.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IEventRepository EventRepository { get; }
        IUserRepository UserRepository { get; }
        IEventRegistrationRepository EventRegistrationRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
