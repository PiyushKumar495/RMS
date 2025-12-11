using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RailwayManagement.Models
{
    public class BookingViewModel
    {
        public int TrainId { get; set; }
        public Trains Train { get; set; }

        [Required]
        public DateTime JourneyDate { get; set; }

        [Required]
        [Range(1, 6, ErrorMessage = "You can book between 1 to 6 tickets.")]
        public int TotalTickets { get; set; }

        public List<PassengerViewModel> Passengers { get; set; }
    }

    public class PassengerViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 100)]
        public int Age { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
