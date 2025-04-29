namespace BookingRoom.DTOs
{
    public class BookingUpdateDto
    {
        public Guid Id { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
