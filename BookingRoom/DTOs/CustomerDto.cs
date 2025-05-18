using System.ComponentModel.DataAnnotations;

namespace BookingRoom.DTOs
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        //[Required]
        //[StringLength(100)]
        public string FullName { get; set; }

        //[Required]
        //[EmailAddress]
        public string Email { get; set; }

        //[Required]
        //[Phone]
        public string Phone { get; set; }

        //[StringLength(200)]
        public string Address { get; set; }

        //[StringLength(50)]
        public string IdentityNumber { get; set; }
    }
}
