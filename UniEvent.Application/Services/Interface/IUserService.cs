using UniEvent.Application.DTOs.User;

namespace UniEvent.Application.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);
        Task<UserResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
