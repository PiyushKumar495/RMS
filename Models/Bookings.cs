using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace RailwayManagement.Models
{
    public partial class Bookings
    {
        public Bookings()
        {
            Passengers = new List<Passengers>();
        }

        [Key]
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public int TrainId { get; set; }
        [Required]
        [Column("PNRNumber")]
        [StringLength(50)]
        public string Pnrnumber { get; set; }
        public int TotalTickets { get; set; }
        public bool BookingStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BookingDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime JourneyDate { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; }
        public bool IsLadiesQuota { get; set; }

        [ForeignKey(nameof(TrainId))]
        [InverseProperty(nameof(Trains.Bookings))]
        public virtual Trains Train { get; set; }
        [ForeignKey(nameof(UserId))]
        [InverseProperty(nameof(Users.Bookings))]
        public virtual Users User { get; set; }
        [InverseProperty("Booking")]
        public virtual List<Passengers> Passengers { get; set; } 
    }
}
