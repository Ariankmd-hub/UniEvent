using Microsoft.AspNetCore.Mvc;
using UniEvent.Application.DTOs.Event;
using UniEvent.Application.Services.Interface;

namespace UniEvent.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken
)
        {
            List<EventResponseDto> events = await _eventService.GetAllAsync(cancellationToken);
            return Ok(events);

        }

        
        [HttpGet("{id}", Name = "GetEventById")]
        public async Task<IActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            EventResponseDto? eventResponseDto = await _eventService.GetByIdAsync(id,cancellationToken);
            if (eventResponseDto == null) { return NotFound(); }
            return Ok(eventResponseDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateEventDto createEventDto,
            CancellationToken cancellationToken
)
        {
            try
            {
                EventResponseDto eventResponseDto = await _eventService.CreateAsync(createEventDto,cancellationToken);


                return CreatedAtRoute(
                                      "GetEventById",
                                      new { id = eventResponseDto.Id },
                                       eventResponseDto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }  

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveAsync(int id, CancellationToken cancellationToken
)
        {
            try
            {
                bool result = await _eventService.RemoveAsync(id,cancellationToken);
                if (!result)
                {
                    return NotFound("Event not found");
                }
                return NoContent();
            }
            catch(KeyNotFoundException ex) 
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
