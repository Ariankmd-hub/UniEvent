
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
        private readonly IEventRegistrationRepository _eventRegistrationRepository;
        public EventService(IEventRepository eventRepository,
            IEventRegistrationRepository eventRegistrationRepository) 
        {
            _eventRepository = eventRepository;
            _eventRegistrationRepository = eventRegistrationRepository;
        }


        public async Task<EventResponseDto> CreateAsync(CreateEventDto createEventDto,
            CancellationToken cancellationToken)
        {
            #region Logic
            if (createEventDto.EventTime < DateTime.UtcNow) 
            {
                throw new ArgumentException("Can't make event for past time ! ");
            }
            if (createEventDto.Capacity < 1) 
            {
                throw new ArgumentException("Capacity can't be lower than 1 ");
            }
            #endregion
            Event eventEntity = new Event
            {
                Title = createEventDto.Title,
                Description = createEventDto.Description,
                EventTime = createEventDto.EventTime,
                Capacity = createEventDto.Capacity,
                Location = createEventDto.Location

            };
            await _eventRepository.AddAsync(eventEntity,cancellationToken);
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

        public async Task<List<EventResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {

            List<Event> events = await _eventRepository.GetAllAsync(cancellationToken);
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

        public async Task<EventResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            Event? eventEntity = await _eventRepository.GetByIdAsync(id, cancellationToken);
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

        public async Task<bool> RemoveAsync(int id,CancellationToken cancellationToken)
        {
            #region Logic 

            Event? eventItem = await _eventRepository.GetByIdAsync(id, cancellationToken);
            if (eventItem == null)
            {
                throw new KeyNotFoundException("Couldn't find the Id that you were loooking for");
            }
            int registeredCount =await _eventRegistrationRepository.GetCountByEventIdAsync(eventItem.Id,cancellationToken);
            if (registeredCount > 0)
            { throw new InvalidOperationException
                    ("Cannot delete event because there are active registrations for it!"); }
            #endregion

            return await _eventRepository.RemoveAsync(id, cancellationToken);
            
        }
    }
}
