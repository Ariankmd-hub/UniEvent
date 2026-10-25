using UniEvent.Application.DTOs.User;
using UniEvent.Application.Services.Interface;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Application.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IEventRegistrationRepository _eventRegistrationRepository;
        public UserService(IUserRepository userRepository,
             IEventRegistrationRepository eventRegistrationRepository)
        {
            _repository = userRepository;
            _eventRegistrationRepository = eventRegistrationRepository;
        }


        public async Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto)
        {
            #region Logic
           bool emailExist = await _repository.IsEmailExistsAsync(createUserDto.EmailAddress);
           if (emailExist) { throw new InvalidOperationException("Already have this email !"); }
            bool universityIdExist = await _repository.IsUniversityIdExistsAsync(createUserDto.UniversityIdNumber);
            if (universityIdExist) 
            {
                throw new InvalidOperationException("User with this university ID already exists !");
            }
           #endregion

            User user = new User()
            {
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                PhoneNumber = createUserDto.PhoneNumber,
                EmailAddress = createUserDto.EmailAddress,
                UniversityIdNumber = createUserDto.UniversityIdNumber

            };
            await _repository.AddAsync(user);
            return new UserResponseDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                UniversityIdNumber = user.UniversityIdNumber
            };
           
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            List<User> users = await _repository.GetAllAsync();
            List<UserResponseDto> responseList = new List<UserResponseDto>();
            foreach (User user in users) 
            {
                UserResponseDto userResponseDto = new UserResponseDto()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    EmailAddress = user.EmailAddress,
                    UniversityIdNumber = user.UniversityIdNumber,
                    Id = user.Id
                };
                responseList.Add(userResponseDto);
            }
            return responseList;
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            User? user =await _repository.GetByIdAsync(id);
            if(user == null) {return null;}
            UserResponseDto responseDto = new UserResponseDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                UniversityIdNumber = user.UniversityIdNumber,
                Id = user.Id
            };
            return responseDto;
        }

        public async Task<bool> RemoveAsync(int id)
        {
            #region Logic
            User? user = await _repository.GetByIdAsync(id);
            if (user == null) { throw new KeyNotFoundException("Could;t find user with this Id"); }
            int eventcount = await _eventRegistrationRepository.GetCountByUserIdAsync(user.Id);
            if (eventcount > 0) 
            {
                throw new InvalidOperationException($"you are in {eventcount} events ,first you need to delete them ! ");
            }
            #endregion
            return await _repository.RemoveAsync(id);
           
        }
    }
}
