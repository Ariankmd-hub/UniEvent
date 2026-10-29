using Microsoft.AspNetCore.Mvc;
using UniEvent.Application.DTOs.EventRegistration;
using UniEvent.Application.Services.Interface;

namespace UniEvent.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventRegistrationController : ControllerBase
    {
        private readonly IEventRegistrationService _eventRegistrationService;
        public EventRegistrationController(IEventRegistrationService eventRegistrationService)
        {
            _eventRegistrationService = eventRegistrationService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken
)
        {
           List<EventRegistrationResponseDto> eventRegistrationResponseDto = 
                await _eventRegistrationService.GetAllAsync(cancellationToken);
            return Ok(eventRegistrationResponseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken) 
        {
            EventRegistrationResponseDto? eventRegistrationResponseDto = 
                await _eventRegistrationService.GetByIdAsync(id, cancellationToken);
            if (eventRegistrationResponseDto == null) { return NotFound(); }
            return Ok(eventRegistrationResponseDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateEventRegistrationDto eventRegistrationDto, 
            CancellationToken cancellationToken) 
        {
            try
            {
                EventRegistrationResponseDto eventRegistrationResponseDto =
                    await _eventRegistrationService.CreateAsync(eventRegistrationDto,cancellationToken);

                return CreatedAtAction(nameof(GetByIdAsync),
                    new { id = eventRegistrationResponseDto.Id }, eventRegistrationResponseDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message }); // 404
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message }); // 400
            }
        }
            
        
       
        [HttpDelete("{id}")]

        public async Task<IActionResult> RemoveAsync(int id, CancellationToken cancellationToken) 
        {
            try
            {
                bool result = await _eventRegistrationService.RemoveAsync(id,cancellationToken);
                if (!result) { return NotFound(); }
                return NoContent();
            }
            catch (KeyNotFoundException ex) 
            {
                return NotFound(new { message = ex.Message });
            }
            catch(InvalidOperationException ex) 
            {
                return BadRequest(new { message = ex.Message });
            }
        
            }
        
    }
}
