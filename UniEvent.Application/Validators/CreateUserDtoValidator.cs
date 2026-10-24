using FluentValidation;
using UniEvent.Application.DTOs.User;
namespace UniEvent.Application.Validators
{
    public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName can't be empty !")
                .MaximumLength(40).WithMessage("lenght Can't be higher than 40 ");

            RuleFor(x => x.LastName)
              .NotEmpty().WithMessage("LastName can't be empty !")
               .MaximumLength(50).WithMessage("lenght Can't be higher than 50 ");

            RuleFor(x => x.PhoneNumber)
              .NotEmpty().WithMessage("PhoneNumber can't be empty !")
               .MaximumLength(12).WithMessage("lenght Can't be higher than 12 ");
            
            RuleFor(x => x.EmailAddress)
              .NotEmpty().WithMessage("Email Address can't be empty !")
               .MaximumLength(50).WithMessage("lenght Can't be higher than 50 ");
            
            RuleFor(x => x.UniversityIdNumber)
              .NotEmpty().WithMessage("University id number can't be empty !")
               .MaximumLength(20).WithMessage("lenght Can't be higher than 20 ");
        }
    }
}
