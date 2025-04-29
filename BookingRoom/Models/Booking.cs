using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public User User { get; set; }

        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending";

        public Payment Payment { get; set; }
    }
}
