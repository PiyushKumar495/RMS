using RailwayManagement.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace RailwayManagement.Models
{
    public partial class Trains
    {
        public Trains()
        {
            Bookings = new HashSet<Bookings>();
        }

        [Key]
        public int TrainId { get; set; }

        [Required]
        [StringLength(100)]
        public string TrainName { get; set; }

        [Required]
        [StringLength(100)]
        public string SourceStation { get; set; }

        [Required]
        [StringLength(100)]
        public string DestinationStation { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DepartureTime { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime ArrivalTime { get; set; }

        public int LadiesSeats { get; set; }
        public int GeneralSeats { get; set; }

        [NotMapped] // This tells EF not to store this in the database
        public int AvailableSeats
        {
            get { return LadiesSeats + GeneralSeats; }
            set { }
        }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal TicketFare { get; set; }

        [InverseProperty("Train")]
        public virtual ICollection<Bookings> Bookings { get; set; }
    }
}