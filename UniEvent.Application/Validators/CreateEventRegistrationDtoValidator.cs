using FluentValidation;
using System.Data;
using UniEvent.Application.DTOs.EventRegistration;

namespace UniEvent.Application.Validators
{
    public class CreateEventRegistrationDtoValidator :AbstractValidator<CreateEventRegistrationDto>
    {
        public CreateEventRegistrationDtoValidator()
        {
            RuleFor(x => x.EventId)
                .GreaterThan(0).WithMessage("EventID must be higher than zero");
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserID must be higher than zero");
        }
    }
}
