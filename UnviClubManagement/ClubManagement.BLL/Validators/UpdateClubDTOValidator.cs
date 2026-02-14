using ClubManagement.BLL.DTO;
using FluentValidation;

namespace ClubManagement.BLL.Validators
{
    public class UpdateClubDTOValidator : AbstractValidator<UpdateClubDTO>
    {
        public UpdateClubDTOValidator()
        {
            RuleFor(c => c.ClubName)
                .NotEmpty().WithMessage("Club Name Is Required");

            RuleFor(c => c.Desc)
                .NotEmpty().WithMessage("Club Description Is Requird");
        }
    }
}
