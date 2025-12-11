using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RailwayManagement.Models
{
    public partial class Users
    {
        public Users()
        {
            Bookings = new HashSet<Bookings>();
        }

        [Key]
        public int UserId { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]  
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; } 

        [Required]
        [Phone] 
        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "User"; 

        [InverseProperty("User")]
        public virtual ICollection<Bookings> Bookings { get; set; }
    }
}
