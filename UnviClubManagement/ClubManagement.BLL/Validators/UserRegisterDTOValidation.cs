using ClubManagement.BLL.DTO;
using FluentValidation;

namespace ClubManagement.BLL.Validators
{
    public class UserRegisterDTOValidation : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDTOValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("First name is required.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(x => x.RoleName)
                .NotEmpty().WithMessage("Role name is required.")
                .Must(role => role == "Student" || role == "ClubLeader").WithMessage("Role name must be either 'Student' or 'ClubLeader'.");
        }
    }
}
