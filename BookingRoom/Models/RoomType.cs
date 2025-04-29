using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class RoomType
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string TypeName { get; set; }

        public string Description { get; set; }

        public ICollection<Room> Rooms { get; set; }
    }
}
