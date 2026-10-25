using Microsoft.AspNetCore.Mvc;
using UniEvent.Application.DTOs.User;
using UniEvent.Application.Services.Interface;

namespace UniEvent.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            List<UserResponseDto> userResponseDtos = await _userService.GetAllAsync();
            return Ok(userResponseDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) 
        {
            UserResponseDto? userResponseDto = await _userService.GetByIdAsync(id);
            if (userResponseDto == null) { return NotFound(); }
            return Ok(userResponseDto);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserDto createUserDto)
        {
            try
            {
                UserResponseDto userResponseDto = await _userService.CreateAsync(createUserDto);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = userResponseDto.Id }, userResponseDto);
            }
            catch (InvalidOperationException ex) 
            { return BadRequest(new { message = ex.Message }); }
            

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id) 
        {
            try
            {
                bool result = await _userService.RemoveAsync(id);
                if (!result) { return NotFound(); }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            { return NotFound(new { message = ex.Message }); }

            catch (InvalidOperationException ex)
            { return BadRequest(new { message = ex.Message }); }
        }
        
    }
}
