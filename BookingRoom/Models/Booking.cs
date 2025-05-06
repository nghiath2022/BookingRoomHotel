using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class Booking
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        // Connect to User
        public Guid UserId { get; set; }
        public User User { get; set; }

        // Connect to Room
        public Guid RoomId { get; set; }
        public Room Room { get; set; }

        // Connect to Customer
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        //Information about booking
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = "Pending";

        public Payment Payment { get; set; }
    }
}
