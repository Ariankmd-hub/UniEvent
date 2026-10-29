
using UniEvent.Application.DTOs.EventRegistration;
using UniEvent.Application.Services.Interface;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Application.Services.Implement
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IEventRegistrationRepository _eventRegistrationRepository;
        private readonly IEventRepository _eventRepository;

        private readonly IUserRepository _userRepository;
        public EventRegistrationService(IEventRegistrationRepository eventRegistrationRepository,
            IEventRepository eventRepository,
            IUserRepository userRepository)
        {
            _eventRegistrationRepository = eventRegistrationRepository;
            _eventRepository = eventRepository;
            _userRepository = userRepository;
        }

        
        public async Task<EventRegistrationResponseDto> CreateAsync(
            CreateEventRegistrationDto createEventRegistrationDto, CancellationToken cancellationToken)
        {
            #region Logic
            Event? eventItem = await _eventRepository.GetByIdAsync(createEventRegistrationDto.EventId,cancellationToken);
            if (eventItem == null) 
            {
                throw new KeyNotFoundException("weren't able to find event , with that Event Id");
            }
            if (eventItem.EventTime < DateTime.UtcNow) 
            {
                throw new ArgumentException("The time for Registring this event is passed !");
            }
           
            User? userItem = await _userRepository.GetByIdAsync(createEventRegistrationDto.UserId,cancellationToken);
            if (userItem == null) 
            {
                throw new KeyNotFoundException("weren't able to find User , with that User Id");
            }

            bool IsAlreadyRegister = await _eventRegistrationRepository.IsAlreadyRegisteredAsync
                                (createEventRegistrationDto.UserId, createEventRegistrationDto.EventId,cancellationToken);
            if (IsAlreadyRegister)
            { throw new InvalidOperationException("User has already registered for this event!"); }

            int registeredCount = await _eventRegistrationRepository.GetCountByEventIdAsync
                (createEventRegistrationDto.EventId, cancellationToken);
            if (registeredCount >= eventItem.Capacity) 
            {
                throw new InvalidOperationException("This event is full! No more seats available.");
            }

            #endregion

            EventRegistration eventRegistration = new EventRegistration()
            {
                UserId = createEventRegistrationDto.UserId,
                EventId = createEventRegistrationDto.EventId,
                RegistrationDate = DateTime.UtcNow

            };
            await _eventRegistrationRepository.AddAsync(eventRegistration,cancellationToken);
            return new EventRegistrationResponseDto
            {
                UserId = eventRegistration.UserId,
                EventId = eventRegistration.EventId,
                RegistrationDate = DateTime.UtcNow,
                Id = eventRegistration.Id
            };
        }

        public async Task<List<EventRegistrationResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<EventRegistration> eventRegistrations = await _eventRegistrationRepository.GetAllAsync(cancellationToken);
            List<EventRegistrationResponseDto> responseList = new List<EventRegistrationResponseDto>();

            foreach (EventRegistration eventRegistration in eventRegistrations) 
            {
                EventRegistrationResponseDto eventRegistrationResponseDto = new EventRegistrationResponseDto()
                {
                    UserId = eventRegistration.UserId,
                    EventId = eventRegistration.EventId,
                    RegistrationDate = eventRegistration.RegistrationDate,
                    Id = eventRegistration.Id
                };
                responseList.Add(eventRegistrationResponseDto);
            };
            return responseList;
            
        }


        public async Task<EventRegistrationResponseDto?> GetByIdAsync(int id,CancellationToken cancellationToken)
        {
            EventRegistration? eventRegistration = await _eventRegistrationRepository.GetByIdAsync(id, cancellationToken);
            if(eventRegistration == null) { return null; }
            return new EventRegistrationResponseDto
            {
                EventId = eventRegistration.EventId,
                UserId = eventRegistration.UserId,
                Id = eventRegistration.Id,
                RegistrationDate = eventRegistration.RegistrationDate
            };
        
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken)
        {
            EventRegistration? eventRegistration = await _eventRegistrationRepository.GetByIdAsync(id, cancellationToken);
            if( eventRegistration == null) 
            { throw new KeyNotFoundException("Couldn't find the the Registration!");  }
            
            Event? eventItem = await _eventRepository.GetByIdAsync(eventRegistration.EventId, cancellationToken);
            if(eventItem != null && eventItem.EventTime < DateTime.UtcNow) 
            {
                throw new InvalidOperationException("Cannot cancel registration for an event that has already occurred!");
            }
       

            return await _eventRegistrationRepository.RemoveAsync(id,cancellationToken);
        }
    }
}
