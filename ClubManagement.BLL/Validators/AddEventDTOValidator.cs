using ClubManagement.BLL.DTO;
using FluentValidation;

namespace ClubManagement.BLL.Validators
{
    public class AddEventDTOValidator : AbstractValidator<AddEventDTO>
    {
        public AddEventDTOValidator()
        {
            RuleFor(e => e.EventName)
                .NotEmpty().WithMessage("Event Name Is Requird");

            RuleFor(e => e.Desc)
                .NotEmpty().WithMessage("Event Description Is Requird");
        }
    }
}
