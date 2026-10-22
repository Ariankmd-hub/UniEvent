
using UniEvent.Application.DTOs.Event;
using UniEvent.Application.Services.Interface;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;
using System.Linq;

namespace UniEvent.Application.Services.Implement
{
    public class EventService : IEventService
    {

        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository) 
        {
            _eventRepository = eventRepository;
        }


        public async Task<EventResponseDto> CreateAsync(CreateEventDto createEventDto)
        {
            Event eventEntity = new Event
            {
                Title = createEventDto.Title,
                Description = createEventDto.Description,
                EventTime = createEventDto.EventTime,
                Capacity = createEventDto.Capacity,
                Location = createEventDto.Location

            };
            await _eventRepository.AddAsync(eventEntity);
            return new EventResponseDto
            {
                Id = eventEntity.Id,
                Title = eventEntity.Title,
                Description = eventEntity.Description,
                Capacity = eventEntity.Capacity,
                Location = eventEntity.Location,
                EventTime = eventEntity.EventTime
            };
        }

        public async Task<List<EventResponseDto>> GetAllAsync()
        {

            List<Event> events = await _eventRepository.GetAllAsync();
            List<EventResponseDto> responseList = new List<EventResponseDto>();
            foreach (Event e in events) 
            {
                EventResponseDto eventResponse = new EventResponseDto()
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    Capacity = e.Capacity,
                    Location = e.Location,
                    EventTime = e.EventTime
                };
                responseList.Add(eventResponse);

            }
            return responseList;
          //return events.Select(e => new EventResponseDto
          //  {
          //      Id = e.Id,
          //      Title = e.Title,
          //      Description = e.Description,
          //      EventTime = e.EventTime,
          //      Capacity = e.Capacity,
          //      Location = e.Location
          //  }).ToList();
        }

        public async Task<EventResponseDto?> GetByIdAsync(int id)
        {
            Event? eventEntity = await _eventRepository.GetByIdAsync(id);
            if (eventEntity == null)
            {
                return null;
            }
            EventResponseDto eventResponseDto = new EventResponseDto()
            {
                Id=eventEntity.Id,
                Title = eventEntity.Title,
                Description= eventEntity.Description,
                Capacity= eventEntity.Capacity,
                Location= eventEntity.Location,
                EventTime= eventEntity.EventTime
                
            };
            return eventResponseDto;
            
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await _eventRepository.RemoveAsync(id);
            
        }
    }
}
