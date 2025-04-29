namespace BookingRoom.DTOs
{
    public class PaymentDto
    {
        public Guid Id { get; set; }
        public string BookingCode { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; }
    }
}
