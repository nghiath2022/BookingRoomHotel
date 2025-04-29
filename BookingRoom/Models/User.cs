using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;


namespace BookingRoom.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }  // Password after encryting. 

        // Foreign Key to table Role
        [Required]
        public Guid RoleId { get; set; }

        public Role Role { get; set; }  // Navigation Property

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Booking> Bookings { get; set; }
    }
}
