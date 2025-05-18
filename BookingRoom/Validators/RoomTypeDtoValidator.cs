using BookingRoom.DTOs;
using FluentValidation;

namespace BookingRoom.Validators
{
    public class RoomTypeDtoValidator :  AbstractValidator<RoomTypeDto>
    {
        public RoomTypeDtoValidator()
        {
            RuleFor(x => x.TypeName)
                .NotEmpty().WithMessage("Room type name is required.")
                .MaximumLength(100);

            RuleFor(x => x.Description)
                .MaximumLength(200);
        }
    }
}
