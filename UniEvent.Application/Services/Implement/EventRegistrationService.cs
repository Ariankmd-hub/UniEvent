
using UniEvent.Application.DTOs.EventRegistration;
using UniEvent.Application.Services.Interface;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Application.Services.Implement
{
    public class EventRegistrationService : IEventRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EventRegistrationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<EventRegistrationResponseDto> CreateAsync(
            CreateEventRegistrationDto createEventRegistrationDto, CancellationToken cancellationToken)
        {
            #region Logic
            Event? eventItem = await _unitOfWork.EventRepository.GetByIdAsync(createEventRegistrationDto.EventId, cancellationToken);
            if (eventItem == null)
            {
                throw new KeyNotFoundException("weren't able to find event , with that Event Id");
            }
            if (eventItem.EventTime < DateTime.UtcNow)
            {
                throw new ArgumentException("The time for Registring this event is passed !");
            }

            User? userItem = await _unitOfWork.UserRepository.GetByIdAsync(createEventRegistrationDto.UserId, cancellationToken);
            if (userItem == null)
            {
                throw new KeyNotFoundException("weren't able to find User , with that User Id");
            }

            bool IsAlreadyRegister = await _unitOfWork.EventRegistrationRepository.IsAlreadyRegisteredAsync
                                (createEventRegistrationDto.UserId, createEventRegistrationDto.EventId, cancellationToken);
            if (IsAlreadyRegister)
            { throw new InvalidOperationException("User has already registered for this event!"); }

            int registeredCount = await _unitOfWork.EventRegistrationRepository.GetCountByEventIdAsync
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
            await _unitOfWork.EventRegistrationRepository.AddAsync(eventRegistration, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
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
            List<EventRegistration> eventRegistrations = await _unitOfWork.EventRegistrationRepository.GetAllAsync(cancellationToken);
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
            }
            ;
            return responseList;

        }


        public async Task<EventRegistrationResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            EventRegistration? eventRegistration = await _unitOfWork.EventRegistrationRepository.GetByIdAsync(id, cancellationToken);
            if (eventRegistration == null) { return null; }
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
            EventRegistration? eventRegistration = await _unitOfWork.EventRegistrationRepository.GetByIdAsync(id, cancellationToken);
            if (eventRegistration == null)
            { throw new KeyNotFoundException("Couldn't find the the Registration!"); }

            Event? eventItem = await _unitOfWork.EventRepository.GetByIdAsync(eventRegistration.EventId, cancellationToken);
            if (eventItem != null && eventItem.EventTime < DateTime.UtcNow)
            {
                throw new InvalidOperationException("Cannot cancel registration for an event that has already occurred!");
            }


            bool result = await _unitOfWork.EventRegistrationRepository.RemoveAsync(id, cancellationToken);
            if (!result)
            {
                return false;
            }
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
