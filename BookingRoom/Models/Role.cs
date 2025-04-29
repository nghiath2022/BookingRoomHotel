using System.ComponentModel.DataAnnotations;

namespace BookingRoom.Models
{
    public class Role
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();  

        [Required]
        [StringLength(100)]
        public string Name { get; set; }   // Role Name : Admin, User, Manager...

        [StringLength(255)]
        public string Description { get; set; }  

        public ICollection<User> Users { get; set; }  // Navigation: 1 Role has many Users
    }
}
