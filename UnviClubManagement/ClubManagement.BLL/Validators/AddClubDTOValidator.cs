using ClubManagement.BLL.DTO;
using FluentValidation;

namespace ClubManagement.BLL.Validators
{
    public class AddClubDTOValidator : AbstractValidator<AddClubDTO>
    {
        public AddClubDTOValidator()
        {
            RuleFor(c => c.ClubName)
                .NotEmpty().WithMessage("Club Name Is Required");

            RuleFor(c => c.Desc)
                .NotEmpty().WithMessage("Club Description Is Required");
        }
    }
}
