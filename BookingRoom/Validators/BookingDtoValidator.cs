using BookingRoom.DTOs;
using FluentValidation;

namespace BookingRoom.Validators
{
    public class BookingDtoValidator : AbstractValidator<BookingCreateDto>
    {
        public BookingDtoValidator()
        {
            RuleFor(x => x.CheckIn)
                .LessThan(x => x.CheckOut)
                .WithMessage("Check-in must be before Check-out.");

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0)
                .WithMessage("Total price must be greater than 0.");

            RuleFor(x => x.RoomId)
                .NotEmpty()
                .WithMessage("RoomId is required.");
            
        }
    }
}
