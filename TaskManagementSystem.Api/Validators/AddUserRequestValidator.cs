using FluentValidation;
using TaskManagementSystem.Common.DTOs.RequestDTO;

namespace TaskManagementSystem.Api.Validators
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequestDto>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Valid email is required.");
            RuleFor(x => x.Role).NotEmpty().WithMessage("Role is required.");                
        }
    }
}
