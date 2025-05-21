namespace BookingRoom.DTOs
{
    public class RoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid RoomTypeId { get; set; }
    }
}
