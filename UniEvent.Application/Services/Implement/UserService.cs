using UniEvent.Application.DTOs.User;
using UniEvent.Application.Services.Interface;
using UniEvent.Domain.Entities;
using UniEvent.Domain.IRepository;

namespace UniEvent.Application.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<UserResponseDto> CreateAsync(CreateUserDto createUserDto,CancellationToken cancellationToken)
        {
            #region Logic
           bool emailExist = await _unitOfWork.UserRepository.
                IsEmailExistsAsync(createUserDto.EmailAddress,cancellationToken);
           if (emailExist) { throw new InvalidOperationException("Already have this email !"); }
            bool universityIdExist = await _unitOfWork.UserRepository.
                IsUniversityIdExistsAsync(createUserDto.UniversityIdNumber,cancellationToken);
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
            await _unitOfWork.UserRepository.AddAsync(user,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
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

        public async Task<List<UserResponseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            List<User> users = await _unitOfWork.UserRepository.GetAllAsync(cancellationToken);
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

        public async Task<UserResponseDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            User? user =await _unitOfWork.UserRepository.GetByIdAsync(id, cancellationToken);
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

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken)
        {
            #region Logic
            User? user = await _unitOfWork.UserRepository.GetByIdAsync(id, cancellationToken);
            if (user == null) { throw new KeyNotFoundException("Could;t find user with this Id"); }
            int eventcount = await _unitOfWork.EventRegistrationRepository.
                GetCountByUserIdAsync(user.Id,cancellationToken);
            if (eventcount > 0) 
            {
                throw new InvalidOperationException($"you are in {eventcount} events ,first you need to delete them ! ");
            }
            #endregion

            bool result = await _unitOfWork.UserRepository.RemoveAsync(id, cancellationToken);
            if (!result) { return  false; }
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
           
        }
    }
}
