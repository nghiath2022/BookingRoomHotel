using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class Payment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid BookingId { get; set; }

        public Booking Booking { get; set; }

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; }

        public string Status { get; set; } = "Pending";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
