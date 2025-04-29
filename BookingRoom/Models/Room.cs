using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class Room
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Name { get; set; }

        public Guid RoomTypeId { get; set; }

        public RoomType RoomType { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Status { get; set; } = "Available";

        public ICollection<Booking> Bookings { get; set; }
    }
}
