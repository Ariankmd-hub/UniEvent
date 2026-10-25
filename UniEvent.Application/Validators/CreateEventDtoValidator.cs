using FluentValidation;
using UniEvent.Application.DTOs.Event;

namespace UniEvent.Application.Validators
{
    public class CreateEventDtoValidator : AbstractValidator<CreateEventDto>
    {
        public CreateEventDtoValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title can't be empty")
                .MaximumLength(30).WithMessage("Title length can't be higher than 30");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description can't be empty!");
            RuleFor(x => x.EventTime)
                .NotEmpty().WithMessage("Even time can't be empty ");
            RuleFor(x => x.Capacity)
                .GreaterThan(0).WithMessage("Capacity must be higher than zero");
            RuleFor(x => x.Location)
               .NotEmpty().WithMessage("Location can't be empty ");

        }

    }
}
