
using UniEvent.Application.DTOs.EventRegistration;
using UniEvent.Application.Services.Interface;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Application.Services.Implement
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IEventRegistrationRepository _eventRegistrationRepository;
        public EventRegistrationService(IEventRegistrationRepository eventRegistrationRepository)
        {
            _eventRegistrationRepository = eventRegistrationRepository;
        }

        
        public async Task<EventRegistrationResponseDto> CreateAsync(CreateEventRegistrationDto createEventRegistrationDto)
        {
            EventRegistration eventRegistration = new EventRegistration()
            {
                UserId = createEventRegistrationDto.UserId,
                EventId = createEventRegistrationDto.EventId,
                RegistrationDate = DateTime.UtcNow

            };
            await _eventRegistrationRepository.AddAsync(eventRegistration);
            return new EventRegistrationResponseDto
            {
                UserId = eventRegistration.UserId,
                EventId = eventRegistration.EventId,
                RegistrationDate = DateTime.UtcNow,
                Id = eventRegistration.Id
            };
        }

        public async Task<List<EventRegistrationResponseDto>> GetAllAsync()
        {
            List<EventRegistration> eventRegistrations = await _eventRegistrationRepository.GetAllAsync();
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


        public async Task<EventRegistrationResponseDto?> GetByIdAsync(int id)
        {
            EventRegistration? eventRegistration = await _eventRegistrationRepository.GetByIdAsync(id);
            if(eventRegistration == null) { return null; }
            return new EventRegistrationResponseDto
            {
                EventId = eventRegistration.EventId,
                UserId = eventRegistration.UserId,
                Id = eventRegistration.Id,
                RegistrationDate = eventRegistration.RegistrationDate
            };
        
        }

        public async Task<bool> RemoveAsync(int id)
        {
            return await _eventRegistrationRepository.RemoveAsync(id);
        }
    }
}
