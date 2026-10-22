using UniEvent.Application.DTOs.User;

namespace UniEvent.Application.Services.Interface
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task<bool> RemoveAsync(int id);
        Task<UserResponseDto?> GetByIdAsync(int id);
    }
}
