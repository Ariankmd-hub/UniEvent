using UniEvent.Application.DTOs.Event;

namespace UniEvent.Application.Services.Interface
{
    public interface IEventService
    {
        Task<List<EventResponseDto>> GetAllAsync();

        Task<EventResponseDto?> GetByIdAsync(int id);
        Task<EventResponseDto> CreateAsync(CreateEventDto createEventDto);
        Task<bool> RemoveAsync(int id);
    }
}
