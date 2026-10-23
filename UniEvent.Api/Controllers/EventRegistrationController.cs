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
        public async Task<IActionResult> GetAllAsync() 
        {
           List<EventRegistrationResponseDto> eventRegistrationResponseDto = 
                await _eventRegistrationService.GetAllAsync();
            return Ok(eventRegistrationResponseDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id) 
        {
            EventRegistrationResponseDto? eventRegistrationResponseDto = 
                await _eventRegistrationService.GetByIdAsync(id);
            if (eventRegistrationResponseDto == null) { return NotFound(); }
            return Ok(eventRegistrationResponseDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateEventRegistrationDto eventRegistrationDto) 
        {
             EventRegistrationResponseDto eventRegistrationResponseDto =
                await _eventRegistrationService.CreateAsync(eventRegistrationDto);
            return CreatedAtAction(nameof(GetByIdAsync),
                new {id = eventRegistrationResponseDto.Id } , eventRegistrationResponseDto);
            
        }
       
        [HttpDelete("{id}")]

        public async Task<IActionResult> RemoveAsync(int id) 
        {
            bool result = await _eventRegistrationService.RemoveAsync(id);
            if(!result) { return NotFound(); }
            return NoContent();
        }
        
    }
}
