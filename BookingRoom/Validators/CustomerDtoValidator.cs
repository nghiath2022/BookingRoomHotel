using BookingRoom.DTOs;
using FluentValidation;

namespace BookingRoom.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\\d{10,15}$").WithMessage("Invalid phone number.");

            RuleFor(x => x.Address)
                .MaximumLength(200);

            RuleFor(x => x.IdentityNumber)
                .MaximumLength(50);
        }
    }
}
