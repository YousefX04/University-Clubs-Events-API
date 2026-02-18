using ClubManagement.BLL.DTO;
using FluentValidation;

namespace ClubManagement.BLL.Validators
{
    public class UpdateEventDTOValidator : AbstractValidator<UpdateEventDTO>
    {
        public UpdateEventDTOValidator()
        {
            RuleFor(e => e.EventName)
                .NotEmpty().WithMessage("Event Name Is Requird");

            RuleFor(e => e.Desc)
                .NotEmpty().WithMessage("Event Description Is Requird");
        }
    }
}
