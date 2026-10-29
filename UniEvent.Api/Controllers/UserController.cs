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
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken
)
        {
            List<UserResponseDto> userResponseDtos = await _userService.GetAllAsync(cancellationToken);
            return Ok(userResponseDtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken) 
        {
            UserResponseDto? userResponseDto = await _userService.GetByIdAsync(id,cancellationToken);
            if (userResponseDto == null) { return NotFound(); }
            return Ok(userResponseDto);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserDto createUserDto, CancellationToken cancellationToken)
        {
            try
            {
                UserResponseDto userResponseDto = await _userService.CreateAsync(createUserDto,cancellationToken);
                return CreatedAtAction(nameof(GetByIdAsync), new { id = userResponseDto.Id }, userResponseDto);
            }
            catch (InvalidOperationException ex) 
            { return BadRequest(new { message = ex.Message }); }
            

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id, CancellationToken cancellationToken
)
        {
            try
            {
                bool result = await _userService.RemoveAsync(id,cancellationToken);
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
