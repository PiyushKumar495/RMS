using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RailwayManagement.Models
{
    public partial class Passengers
    {
        [Key]
        public int PassengerId { get; set; }
        public int BookingId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }

        [ForeignKey(nameof(BookingId))]
        [InverseProperty(nameof(Bookings.Passengers))]
        public virtual Bookings Booking { get; set; }
    }
}
