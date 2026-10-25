
using UniEvent.Application.DTOs.EventRegistration;

namespace UniEvent.Application.Services.Interface
{
    public interface IEventRegistrationService
    {
        Task<List<EventRegistrationResponseDto>> GetAllAsync();
        Task<EventRegistrationResponseDto> CreateAsync(CreateEventRegistrationDto createEventRegistrationDto);

        Task<EventRegistrationResponseDto?> GetByIdAsync(int id);
        Task<bool> RemoveAsync(int id);
    }
}
