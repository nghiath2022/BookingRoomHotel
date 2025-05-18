using BookingRoom.DTOs;
using FluentValidation;

namespace BookingRoom.Validators
{
    public class PaymentDtoValidator : AbstractValidator<PaymentCreateDto>
    {
        public PaymentDtoValidator()
        {
            RuleFor(x => x.BookingId)
                .NotEmpty();

            RuleFor(x => x.Amount)
                .GreaterThan(0);
        }
    }
}
