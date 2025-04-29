namespace BookingRoom.DTOs
{
    public class PaymentCreateDto
    {
        public Guid BookingId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
