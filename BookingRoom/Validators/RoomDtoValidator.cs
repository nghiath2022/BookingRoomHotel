using BookingRoom.DTOs;
using FluentValidation;

namespace BookingRoom.Validators
{
    public class RoomDtoValidator : AbstractValidator<RoomDto>
    {
        public RoomDtoValidator()
        {
            // Name validation
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Room name is required.")
                .MaximumLength(100).WithMessage("Room name cannot exceed 100 characters.");

            // Price validation
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required.")
                .GreaterThan(0).WithMessage("Price must be greater than 0.")
                .ScalePrecision(2, 18).WithMessage("Price must have up to 2 decimal places.");

            // Description validation (optional)
            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            // Status validation
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(status => IsValidStatus(status)).WithMessage("Invalid room status.");

            // RoomTypeId validation
            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("RoomTypeId is required.")
                .NotEqual(Guid.Empty).WithMessage("RoomTypeId cannot be empty.");
        }

        private bool IsValidStatus(string status)
        {
            // Chỉ chấp nhận các status hợp lệ
            var validStatuses = new[] { "Available", "Occupied", "Maintenance" };
            return validStatuses.Contains(status);
        }
    }
}
