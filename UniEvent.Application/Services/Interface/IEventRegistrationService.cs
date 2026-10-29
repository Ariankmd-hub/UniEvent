
using UniEvent.Application.DTOs.EventRegistration;

namespace UniEvent.Application.Services.Interface
{
    public interface IEventRegistrationService
    {
        Task<List<EventRegistrationResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<EventRegistrationResponseDto> CreateAsync(CreateEventRegistrationDto createEventRegistrationDto,
            CancellationToken cancellationToken);

        Task<EventRegistrationResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);
    }
}
