using UniEvent.Application.DTOs.Event;

namespace UniEvent.Application.Services.Interface
{
    public interface IEventService
    {
        Task<List<EventResponseDto>> GetAllAsync(CancellationToken cancellationToken);

        Task<EventResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<EventResponseDto> CreateAsync(CreateEventDto createEventDto, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);
    }
}
