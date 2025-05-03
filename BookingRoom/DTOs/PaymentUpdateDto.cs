namespace BookingRoom.DTOs
{
    public class PaymentUpdateDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}
