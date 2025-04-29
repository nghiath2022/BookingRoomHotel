namespace BookingRoom.DTOs
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public string UserFullName { get; set; }
        public string RoomName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
